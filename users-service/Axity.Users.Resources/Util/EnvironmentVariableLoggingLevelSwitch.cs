// <summary>
// <copyright file="EnvironmentVariableLoggingLevelSwitch.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Resources.Util
{
    using System;
    using Serilog;
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Get LogEventLevel.
    /// </summary>
    public class EnvironmentVariableLoggingLevelSwitch : LoggingLevelSwitch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentVariableLoggingLevelSwitch"/> class.
        /// </summary>
        /// <param name="environmentVariable">EnvironmentVariable Collection.</param>
        public EnvironmentVariableLoggingLevelSwitch(string environmentVariable)
        {
            if (Enum.TryParse<LogEventLevel>(Environment.ExpandEnvironmentVariables(environmentVariable), true, out LogEventLevel level))
            {
                this.MinimumLevel = level;
            }
        }

        /// <summary>
        /// LogErrorSequenceRequest.
        /// </summary>
        /// <param name="response">ExceptionContext context.</param>
        /// <param name="logger">ILogger logger.</param>
        public static void LogErrorSequenceRequest(HttpResponseMessage response, ILogger logger)
        {
            logger.Error("Se intento la conexión {RequstUri}", response.RequestMessage.RequestUri);
            logger.Error("El reasion phrase {RequstUri}", response.ReasonPhrase);
            logger.Error("Fecha de solicitud {Date}", response.Headers.Date);
            logger.Error("Elstatus code fue {StatusCode}", response.StatusCode);
            logger.Error("Content string {ContentString}", response.Content.ReadAsStringAsync().Result);
        }
    }
}
