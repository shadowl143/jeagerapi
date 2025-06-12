// </summary>
// <copyright file="UserCopyService.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Services.UserCopy.Impl
{
    using System.Net.Http.Json;
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;
    using Axity.Users.Services.Util;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    /// <summary>
    /// User copy master.
    /// </summary>
    public class UserCopyService : IUserCopyService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;
        private readonly IConfiguration conf;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCopyService"/> class.
        /// </summary>
        /// <param name="httpClient">httpClient.</param>
        /// <param name="logger">ILogger logger.</param>
        /// <param name="conf">IConfiguration.</param>
        public UserCopyService(HttpClient httpClient, ILogger logger, IConfiguration conf)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            this.conf = conf;
        }

        /// <inheritdoc/>
        public async Task<ServiceResponse<UserDto>> GetById(int id)
        {
            var httpResponse = await this.httpClient.GetAsync($"{this.conf.GetSection("UserCopy").Value.ToString()}/{id}/id");
            ValidationStatusCodeUtil.ValidationStatusCode(httpResponse, this.logger);
            ServiceResponse<UserDto> user = await httpResponse.Content.ReadFromJsonAsync<ServiceResponse<UserDto>>();
            return user;
        }

        /// <inheritdoc/>
        public async Task<PaginatorServiceDto<UserDto>> Paginator(int rows, int page)
        {
            var httpResponse = await this.httpClient.GetAsync($"user?page={page}&size={rows}");
            ValidationStatusCodeUtil.ValidationStatusCode(httpResponse, this.logger);
            PaginatorServiceDto<UserDto> user = await httpResponse.Content.ReadFromJsonAsync<PaginatorServiceDto<UserDto>>();
            return user;
        }
    }
}
