// <summary>
// <copyright file="UserService.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Services.User.Impl
{
    using AutoMapper;
    using Axity.Users.DataAccess.User;
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;
    using Axity.Users.Entities.Model.User;

    /// <summary>
    /// Class UserService.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserDao modelDao;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="modelDao">IUserDao modelDao.</param>
        /// <param name="mapper">IMapper mapper.</param>
        public UserService(IUserDao modelDao, IMapper mapper)
        {
            this.modelDao = modelDao;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<UserDto> Create(UserDto model)
        {
            var modelToSave = this.mapper.Map<UserModel>(model);
            var result = await this.modelDao.Create(modelToSave);
            return this.mapper.Map<UserDto>(result);
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            await this.modelDao.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetById(int id)
        {
            var getById = await this.modelDao.GetById(id);
            return this.mapper.Map<UserDto>(getById);
        }

        /// <inheritdoc/>
        public async Task<PaginatorServiceDto<UserDto>> Paginator(int rows, int page)
        {
            List<UserModel> data = await this.modelDao.Paginator(rows, page);
            int count = await this.modelDao.Count();
            var table = new PaginatorServiceDto<UserDto>
            {
                Rows = this.mapper.Map<List<UserDto>>(data),
                TotalRow = count,
                Page = page,
                Size = rows,
            };
            return table;
        }

        /// <inheritdoc/>
        public async Task<UserDto> Update(UserDto model)
        {
            var modelToUpdate = this.mapper.Map<UserModel>(model);
            var result = await this.modelDao.Update(modelToUpdate);
            return this.mapper.Map<UserDto>(result);
        }
    }
}
