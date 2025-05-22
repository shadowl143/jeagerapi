// <summary>
// <copyright file="IUserFacade.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Facade.User
{
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;

    /// <summary>
    /// Interface User Facade.
    /// </summary>
    public interface IUserFacade
    {
        /// <summary>
        /// Get all with paginate.
        /// </summary>
        /// <param name="rows">info row.</param>
        /// <param name="page">page.</param>
        /// <returns>list user model.</returns>
        Task<PaginatorServiceDto<UserDto>> Paginator(int rows, int page);

        /// <summary>
        /// get by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>user model.</returns>
        Task<ServiceResponse<UserDto>> GetById(int id);

        /// <summary>
        /// crete new user model.
        /// </summary>
        /// <param name="model">object User Model.</param>
        /// <returns>User model.</returns>
        Task<ServiceResponse<UserDto>> Create(UserDto model);

        /// <summary>
        /// Update row.
        /// </summary>
        /// <param name="model">object User Model.</param>
        /// <returns>User model updated.</returns>
        Task<ServiceResponse<UserDto>> Update(UserDto model);

        /// <summary>
        /// Delete row.
        /// </summary>
        /// <param name="id">primary key row.</param>
        /// <returns>Task.</returns>
        Task Delete(int id);
    }
}
