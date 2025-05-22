// <copyright file="IUserDao.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>

namespace Axity.Users.DataAccess.User
{
    using Axity.Users.Entities.Model.User;

    /// <summary>
    /// interface user dao.
    /// </summary>
    public interface IUserDao
    {
        /// <summary>
        /// Get all with paginate.
        /// </summary>
        /// <param name="rows">info row.</param>
        /// <param name="page">page.</param>
        /// <returns>list user model.</returns>
        Task<List<UserModel>> Paginator(int rows, int page);

        /// <summary>
        /// count.
        /// </summary>
        /// <returns>int total row.</returns>
        Task<int> Count();

        /// <summary>
        /// get by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>user model.</returns>
        Task<UserModel?> GetById(int id);

        /// <summary>
        /// crete new user model.
        /// </summary>
        /// <param name="model">object User Model.</param>
        /// <returns>User model.</returns>
        Task<UserModel> Create(UserModel model);

        /// <summary>
        /// Update row.
        /// </summary>
        /// <param name="model">object User Model.</param>
        /// <returns>User model updated.</returns>
        Task<UserModel> Update(UserModel model);

        /// <summary>
        /// Delete row.
        /// </summary>
        /// <param name="id">primary key row.</param>
        /// <returns>Task.</returns>
        Task Delete(int id);
    }
}
