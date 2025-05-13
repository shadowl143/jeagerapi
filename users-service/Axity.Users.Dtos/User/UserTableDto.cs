// <summary>
// <copyright file="UserTableDto.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Dtos.User
{
    /// <summary>
    /// class UserTableDto.
    /// </summary>
    public class UserTableDto
    {
        /// <summary>
        /// Gets or sets int Id.
        /// </summary>
        /// <value>int Id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets string NameUser.
        /// </summary>
        /// <value>string NameUser.</value>
        public string NameUser { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets deecimal Age.
        /// </summary>
        /// <value>decimal Age.</value>
        public decimal Age { get; set; }
    }
}
