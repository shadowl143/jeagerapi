// <summary>
// <copyright file="UserCopyController.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;
    using Axity.Users.Facade.UserCopy;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// User copy.
    /// </summary>
    [Route("usercopy")]
    [ApiController]
    public class UserCopyController : ControllerBase
    {
        private readonly IUserCopyFacade modelFacade;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCopyController"/> class.
        /// </summary>
        /// <param name="modelFacade">IUserCopyFacade modelFacade.</param>
        public UserCopyController(IUserCopyFacade modelFacade)
        {
            this.modelFacade = modelFacade;
        }

        /// <summary>
        /// Paginator Method user.
        /// </summary>
        /// <response code="200">Get list user.</response>
        /// <param name="size">size.</param>
        /// <param name="page">page.</param>
        /// <returns>paginator user.</returns>
        [ProducesResponseType(typeof(PaginatorServiceDto<UserDto>), StatusCodes.Status200OK, "application/json")]
        [HttpGet]
        public async Task<PaginatorServiceDto<UserDto>> Paginator([FromQuery][Required] int size, [FromQuery][Required] int page)
        {
            return await this.modelFacade.Paginator(size, page);
        }

        /// <summary>
        /// Paginator Method user.
        /// </summary>
        /// <response code="200">Get list user.</response>
        /// <param name="id">int id.</param>
        /// <returns>paginator user.</returns>
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status404NotFound, "application/json")]
        [HttpGet("{id}/id")]
        public async Task<ServiceResponse<UserDto>> GetById([FromRoute] int id)
        {
            return await this.modelFacade.GetById(id);
        }
    }
}
