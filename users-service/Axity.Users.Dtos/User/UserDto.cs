// <summary>
// <copyright file="UserDto.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Dtos.User
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// class User model.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets primary key id.
        /// </summary>
        /// <value>id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        /// <value>string Name.</value>
        [Required(ErrorMessage = "El campos {0} es requerido")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets string SurName.
        /// </summary>
        /// <value>string Surname.</value>
        [Required(ErrorMessage = "El campos {0} es requerido")]
        public string SurName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets string SurName.
        /// </summary>
        /// <value>string Surname.</value>
        [Required(ErrorMessage = "El campos {0} es requerido")]
        public decimal Age { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets status.
        /// </summary>
        /// <value>bool status.</value>
        [DefaultValue(true)]
        public bool Status { get; set; }
    }
}
