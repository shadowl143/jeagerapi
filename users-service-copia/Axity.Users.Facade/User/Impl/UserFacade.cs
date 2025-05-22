// <summary>
// <copyright file="UserFacade.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Facade.User.Impl
{
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;
    using Axity.Users.Services.User;

    /// <summary>
    /// User Facade class.
    /// </summary>
    public class UserFacade : IUserFacade
    {
        private readonly IUserService modelService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFacade"/> class.
        /// </summary>
        /// <param name="modelService">IUserService modelService.</param>
        public UserFacade(IUserService modelService)
        {
            this.modelService = modelService;
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<UserDto>> Create(UserDto model)
        {
            var create = await this.modelService.Create(model);
            return new ServiceResponse<UserDto>(create) { StatusCode = System.Net.HttpStatusCode.Created, Message = "objeto creado" };
        }

        /// <inheritdoc/>
        public async Task Delete(int id)
        {
            await this.modelService.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<UserDto>> GetById(int id)
        {
            var getById = await this.modelService.GetById(id);
            return new ServiceResponse<UserDto>(getById);
        }

        /// <inheritdoc/>
        public Task<PaginatorServiceDto<UserDto>> Paginator(int rows, int page)
        {
            return this.modelService.Paginator(rows, page);
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<UserDto>> Update(UserDto model)
        {
            var update = await this.modelService.Update(model);
            return new ServiceResponse<UserDto>(update);
        }
    }
}
