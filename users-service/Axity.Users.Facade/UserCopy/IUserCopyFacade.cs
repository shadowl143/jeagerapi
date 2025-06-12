// <summary>
// <copyright file="IUserCopyFacade.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>
namespace Axity.Users.Facade.UserCopy
{
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;

    /// <summary>
    /// Interface user copy.
    /// </summary>
    public interface IUserCopyFacade
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
    }
}
