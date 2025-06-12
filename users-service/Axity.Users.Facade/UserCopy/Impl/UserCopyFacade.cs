// <summary>
// <copyright file="UserCopyFacade.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Facade.UserCopy.Impl
{
    using System.Threading.Tasks;
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;
    using Axity.Users.Services.UserCopy;

    /// <summary>
    /// UserCopy facade.
    /// </summary>
    public class UserCopyFacade : IUserCopyFacade
    {
        private readonly IUserCopyService modelService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCopyFacade"/> class.
        /// </summary>
        /// <param name="modelService">IUserCopyService modelService.</param>
        public UserCopyFacade(IUserCopyService modelService)
        {
            this.modelService = modelService;
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<UserDto>> GetById(int id)
        {
            return await this.modelService.GetById(id);
        }

        /// <inheritdoc/>
        public Task<PaginatorServiceDto<UserDto>> Paginator(int rows, int page)
        {
            return this.modelService.Paginator(rows, page);
        }
    }
}
