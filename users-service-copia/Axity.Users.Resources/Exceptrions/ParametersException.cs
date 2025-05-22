// <summary>
// <copyright file="ParametersException.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Resources.Exceptrions
{
    /// <summary>
    /// Parameter exception.
    /// </summary>
    public class ParametersException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersException"/> class.
        /// </summary>
        public ParametersException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersException"/> class.
        /// </summary>
        /// <param name="message">Message Exception.</param>
        public ParametersException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersException"/> class.
        /// </summary>
        /// <param name="message">Message Exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public ParametersException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
