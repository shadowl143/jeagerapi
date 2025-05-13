// <summary>
// <copyright file="SqlValidationHelper.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// <author>Code Reviewer</author>
// <date>3/1/2022</date>
// </summary>

namespace Axity.Users.DataAccess.Helper
{
    using Axity.Users.Resources.Exceptrions;

    /// <summary>
    /// Sql validation.
    /// </summary>
    /// <typeparam name="T">Generic class.</typeparam>
    public static class SqlValidationHelper<T>
        where T : class
    {
        /// <summary>
        /// Validate if a register exist in the database.
        /// </summary>
        /// <param name="entity">Class generic.</param>
        /// <param name="name">name data found.</param>
        /// <returns>Generic.</returns>
        /// <exception cref="NotFoundException">When a register not found.</exception>
        public static T ValidateFound(T? entity, string name)
        {
            if (entity == null)
            {
                throw new NotFoundException($"No se localizo el registro {name}");
            }

            return entity;
        }

        /// <summary>
        /// Validate if a register exist in the database.
        /// </summary>
        /// <param name="entity">Class generic.</param>
        /// <param name="name">name data found.</param>
        /// <returns>Generic.</returns>
        /// <exception cref="NotFoundException">When a register not found.</exception>
        public static List<T> VailidateCountList(List<T> entity, string name)
        {
            if (entity.Count == 0)
            {
                throw new NotFoundException($"No se localizaron registros de {name}");
            }

            return entity;
        }

        /// <summary>
        /// Validate if the data exist in the table.
        /// </summary>
        /// <param name="exist">result query.</param>
        /// <param name="column">column´s name.</param>
        /// <exception cref="BusinessException">Data repeat in the table.</exception>
        public static void ExistRow(bool exist, string column)
        {
            if (exist)
            {
                throw new BusinessException($"El {column} ya se encuentra registrado");
            }
        }
    }
}
