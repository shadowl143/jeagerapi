// <summary>
// <copyright file="TestUtil.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Test.Util
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Axity.Users.Entities.Context;
    using Axity.Users.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;

    /// <summary>
    /// utilities for test.
    /// </summary>
    public static class TestUtil
    {
        /// <summary>
        /// Configuration static.
        /// </summary>
        /// <returns>Iconfiguration.</returns>
        public static IConfiguration MockConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                { "bankbridge:url", "http://localhost/api" },
                { "bankbridge:urlbankbridge", "apitoken" },
                { "origenbanner:web", "servalum" },
                { "origenbanner:android", "servalum-android" },
                { "MAILID", "1023232" },
                { "origenbanner:ios", "servalum-ios" },
                { "webhook:codi", "http://localhost/api/codi" },
                { "urlredirection:market", "exm:portal" },
                { "webhook:market", "http://localhost/api/market" },
                { "webhook", "http://localhost/webhook/reciver" },
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(initialData: inMemorySettings).Build();
            return configuration;
        }

        /// <summary>
        /// http client mock.
        /// </summary>
        /// <param name="content">Strign context or JsonCreate.</param>
        /// <param name="code">code.</param>
        /// <returns>Httpclient.</returns>
        public static HttpClient MockHttpCliente(dynamic content, HttpStatusCode code)
        {
            Mock<HttpMessageHandler> handler = new Mock<HttpMessageHandler>();
            var mockHttpResponse = new HttpResponseMessage()
            {
                Content = content,
                StatusCode = code,
            };

            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockHttpResponse);

            var httpClientSuccess = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("http://localhost"),
            };
            return httpClientSuccess;
        }

        /// <summary>
        /// Automapper serilog.
        /// </summary>
        /// <returns>IMapper.</returns>
        public static IMapper CreateAutomapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            return mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// DataBase context.
        /// </summary>
        /// <param name="name">string name.</param>
        /// <returns>DbContextOptions.<DatabaseContext>.</returns>
        public static DatabaseContext DataBaseContext(string name)
        {
            var option = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
            return new DatabaseContext(option);
        }
    }
}
