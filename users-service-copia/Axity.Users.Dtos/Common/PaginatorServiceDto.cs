// <copyright file="PaginatorServiceDto.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>

namespace Axity.Users.Dtos.Common
{
    /// <summary>
    /// Paginator class.
    /// </summary>
    /// <typeparam name="T">Generic.</typeparam>
    public class PaginatorServiceDto<T>
    {
        /// <summary>
        /// Gets or sets int page.
        /// </summary>
        /// <value>int page.</value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets int size.
        /// </summary>
        /// <value>int Size.</value>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets int total row.
        /// </summary>
        /// <value>int Total row.</value>
        public int TotalRow { get; set; }

        /// <summary>
        /// Gets int total row.
        /// </summary>
        /// <value>int Total row.</value>
        public int Pages
        {
            get
            {
                var data = Math.Ceiling((double)this.TotalRow / (double)this.Size);
                return (int)data;
            }
        }

        /// <summary>
        /// Gets or sets List of row.
        /// </summary>
        /// <value>rows.</value>
        public List<T> Rows { get; set; } = new List<T>();
    }
}
