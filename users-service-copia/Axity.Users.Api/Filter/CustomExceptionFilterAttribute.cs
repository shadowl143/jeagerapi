// <summary>
// <copyright file="CustomExceptionFilterAttribute.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Api.Filter
{
    using System.Net;
    using System.Text;
    using Axity.Users.Dtos.Common;
    using Axity.Users.Resources.Exceptrions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Serilog;

    /// <summary>
    /// Custom Exception Filter Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, (HttpStatusCode status, string message)> exceptionMappings;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilterAttribute"/> class.
        /// </summary>
        /// <param name="logger">Serilog.</param>
        public CustomExceptionFilterAttribute(ILogger logger)
        {
            this.logger = logger;

            // Inicializamos un diccionario con mapeo de tipos de excepciones comunes a su código de estado y mensaje
            this.exceptionMappings = new Dictionary<Type, (HttpStatusCode status, string message)>
            {
                { typeof(ForbiddenException), (HttpStatusCode.Forbidden, "Acceso prohibido") },
                { typeof(ParametersException), (HttpStatusCode.BadRequest, "Parámetros incorrectos") },
                { typeof(CustomServiceException), (HttpStatusCode.BadRequest, "Error en el servicio") },
                { typeof(NotFoundException), (HttpStatusCode.NotFound, "Recurso no encontrado") },
                { typeof(BusinessException), (HttpStatusCode.Conflict, "Conflicto de negocio") },
            };
        }

        /// <summary>
        /// Método que se ejecuta cuando se produce una excepción.
        /// </summary>
        /// <param name="context">OnException.</param>
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "Servicio no disponible. Intente más tarde o contacte al administrador.";
            string messageError = string.Empty;
            int codeError = 0;

            if (this.exceptionMappings.ContainsKey(exception.GetType()))
            {
                // Mapeamos el tipo de excepción a un código de estado y mensaje
                var mapping = this.exceptionMappings[exception.GetType()];
                status = mapping.status;
                message = exception.Message;
            }
            else
            {
               this.HandleSpecificExceptionTypes(exception, ref codeError, ref messageError);
            }

            // Generamos la respuesta JSON
            this.BuildErrorResponse(context, status, message, messageError);
        }

        private void HandleSpecificExceptionTypes(Exception exception, ref int codeError, ref string messageError)
        {
            // Manejo de excepciones más específicas
            if (exception is DbUpdateException updateException)
            {
                this.logger.Error(updateException, "Error en base de datos.");
                var sqlException = updateException.InnerException as Microsoft.Data.SqlClient.SqlException;
                if (sqlException != null)
                {
                    codeError = sqlException.Number;
                }
            }
            else if (exception is HttpRequestException httpRequestException)
            {
                this.logger.Error(httpRequestException, "Error en solicitud HTTP.");
                var socketException = httpRequestException.InnerException as System.Net.Sockets.SocketException;
                if (socketException != null)
                {
                    codeError = socketException.ErrorCode;
                }
            }
            else
            {
                // Caso por defecto si la excepción no está en el mapeo
                this.logger.Error(exception, "Error desconocido.");
                var encryptedError = Encoding.UTF8.GetBytes(exception.Message);
                messageError = Convert.ToBase64String(encryptedError);
            }
        }

        private void BuildErrorResponse(ExceptionContext context, HttpStatusCode status, string message, string messageError)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            var inner = context.Exception.InnerException == null ? message : JsonConvert.SerializeObject(context.Exception.InnerException.Message.Split("\r\n"));
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            var serviceResponse = JsonConvert.SerializeObject(new ServiceResponse<string>(null)
            {
                StatusCode = status,
                Message = message,
                MessageError = messageError,
            });

            response.WriteAsync(serviceResponse);
            var logMessage = $"ErrorType: {context.Exception.GetType()} Message: {inner}";
            this.logger.Error(logMessage);
        }
    }
}
