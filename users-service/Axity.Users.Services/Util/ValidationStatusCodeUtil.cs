// <summary>
// <copyright file="ValidationStatusCodeUtil.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Services.Util
{
    using System.Net;
    using System.Net.Http;
    using Axity.Users.Resources.Exceptrions;
    using Axity.Users.Resources.Util;

    /// <summary>
    /// Validator status code on request http.
    /// </summary>
    public static class ValidationStatusCodeUtil
    {
        /// <summary>
        /// Validate status code.
        /// </summary>
        /// <param name="response">response status code.</param>
        /// <param name="logger">serilog.</param>
        /// <exception cref="NotFoundException">Not Found exception.</exception>
        /// <exception cref="UnauthorizedException">Unauthorized exception.</exception>
        public static void ValidationStatusCode(HttpResponseMessage response, Serilog.ILogger logger)
        {
            logger.Information("validar el status code de la petición");
            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.ExpectationFailed)
            {
                logger.Error("No se localizo el elemento");
                EnvironmentVariableLoggingLevelSwitch.LogErrorSequenceRequest(response, logger);
                throw new NotFoundException("Error de comunicación favor consulte al admin");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                logger.Error("No se tiene permisos con las credenciales usadas.");
                EnvironmentVariableLoggingLevelSwitch.LogErrorSequenceRequest(response, logger);
                throw new UnauthorizedAccessException("No tiene permisos para realizar la consulta");
            }
            else if ((int)response.StatusCode < (int)HttpStatusCode.OK || (int)response.StatusCode > 299)
            {
                logger.Error("La respuesta retorno un error => {0}", response.Content.ReadAsStringAsync().Result);
                EnvironmentVariableLoggingLevelSwitch.LogErrorSequenceRequest(response, logger);
                throw new CustomServiceException($"El servicio no esta disponible por el momento");
            }
        }
    }
}
