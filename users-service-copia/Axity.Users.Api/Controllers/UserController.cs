// <summary>
// <copyright file="UserController.cs" company="Axity">
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
    using Axity.Users.Facade.User;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    /// <summary>
    /// User Controller.
    /// </summary>
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade modelFacade;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="modelFacade">IUserFacade modelFacade.</param>
        /// <param name="logeer">ILogger logeer.</param>
        public UserController(IUserFacade modelFacade, Serilog.ILogger logger)
        {
            this.modelFacade = modelFacade;
            this.logger = logger;
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
            this.logger.Information("hola mundo");
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

        /// <summary>
        /// Paginator Method user.
        /// </summary>
        /// <response code="201">create new user.</response>
        /// <response code="400">internal error.</response>
        /// <param name="model">User model.</param>
        /// <returns>paginator user.</returns>
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status201Created, "application/json")]
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status400BadRequest, "application/json")]
        [HttpPost]
        public async Task<ServiceResponse<UserDto>> Create([FromBody] UserDto model)
        {
            return await this.modelFacade.Create(model);
        }

        /// <summary>
        /// Paginator Method user.
        /// </summary>
        /// <response code="202">update user.</response>
        /// <response code="404">Not found.</response>
        /// <param name="model">User model.</param>
        /// <returns>paginator user.</returns>
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status202Accepted, "application/json")]
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status404NotFound, "application/json")]
        [HttpPut]
        public async Task<ServiceResponse<UserDto>> Update([FromBody] UserDto model)
        {
            return await this.modelFacade.Update(model);
        }

        /// <summary>
        /// Paginator Method user.
        /// </summary>
        /// <response code="201">create new user.</response>
        /// <response code="400">Error internal.</response>
        /// <response code="404">Not found.</response>
        /// <param name="id">User model.</param>
        /// <returns>paginator user.</returns>
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status204NoContent, "application/json")]
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status400BadRequest, "application/json")]
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status404NotFound, "application/json")]
        [ProducesResponseType(typeof(ServiceResponse<UserDto>), StatusCodes.Status404NotFound, "application/json")]
        [HttpDelete]
        public async Task Delete([FromBody] int id)
        {
            await this.modelFacade.Delete(id);
        }
    }
}
