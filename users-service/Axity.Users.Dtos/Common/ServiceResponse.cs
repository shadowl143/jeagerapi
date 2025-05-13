// <summary>
// <copyright file="ServiceResponse.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Dtos.Common
{
    using System.Net;

    /// <summary>
    /// Service response.
    /// </summary>
    /// <typeparam name="T">object T.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ServiceResponse{T}"/> class.
    /// </remarks>
    public class ServiceResponse<T>(T data)
    {
        /// <summary>
        /// Gets or sets Message.
        /// </summary>
        /// <value>string Message.</value>
        public string Message { get; set; } = "Success.";

        /// <summary>
        /// Gets or sets MessageError.
        /// </summary>
        /// <value>string MessageError.</value>
        public string MessageError { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets StatusCode.
        /// </summary>
        /// <value>object HttpstatusCode.</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Gets or sets Generic data.
        /// </summary>
        /// <value>Object generic.</value>
        public T Data { get; set; } = data;
    }
}
