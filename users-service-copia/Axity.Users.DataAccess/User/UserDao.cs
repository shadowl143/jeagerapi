// <copyright file="UserDao.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>

namespace Axity.Users.DataAccess.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Axity.Users.DataAccess.Helper;
    using Axity.Users.Entities.Context;
    using Axity.Users.Entities.Model.User;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// class user dao.
    /// </summary>
    /// <param name="databaseContext">object IDatabaseContext.</param>
    public class UserDao(IDatabaseContext databaseContext)
        : IUserDao
    {
        /// <inheritdoc/>
        public Task<int> Count()
        {
            var counter = databaseContext.Users.AsNoTracking().CountAsync();
            return counter;
        }

        /// <inheritdoc/>
        public async Task<UserModel> Create(UserModel model)
        {
            try
            {
                await databaseContext.Users.AddAsync(model);
                await ((DatabaseContext)databaseContext).SaveChangesAsync();
                return model;
            }
            catch (DbUpdateException ex)
            {
               throw new DbUpdateException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            var row = await databaseContext.Users.Where(wr => wr.Id == id).FirstOrDefaultAsync();
            SqlValidationHelper<UserModel>.ValidateFound(row, "Usuario");
            databaseContext.Users.Remove(row);
            await ((DatabaseContext)databaseContext).SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<UserModel> GetById(int id)
        {
            var row = await databaseContext.Users.Where(wr => wr.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return row;
        }

        /// <inheritdoc/>
        public async Task<List<UserModel>> Paginator(int rows, int page)
        {
            var users = await databaseContext.Users.Skip((page - 1) * rows).Take(rows).AsNoTracking().ToListAsync();
            SqlValidationHelper<UserModel>.VailidateCountList(users, "Usuarios");
            return users;
        }

        /// <inheritdoc/>
        public async Task<UserModel> Update(UserModel model)
        {
            var row = await databaseContext.Users.Where(wr => wr.Id == model.Id).FirstOrDefaultAsync();
            SqlValidationHelper<UserModel>.ValidateFound(row, "Usuario");
            row.Name = model.Name;
            row.SurName = model.SurName;
            row.Age = model.Age;
            await ((DatabaseContext)databaseContext).SaveChangesAsync();
            return row;
        }
    }
}
