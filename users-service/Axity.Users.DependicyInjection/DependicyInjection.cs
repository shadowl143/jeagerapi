// <summary>
// <copyright file="DependicyInjection.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.DependicyInjection
{
    using AutoMapper;
    using Axity.Users.DataAccess.User;
    using Axity.Users.Entities.Context;
    using Axity.Users.Facade.User;
    using Axity.Users.Facade.User.Impl;
    using Axity.Users.Services.Mapping;
    using Axity.Users.Services.User;
    using Axity.Users.Services.User.Impl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    /// <summary>
    /// Dependicy Injection.
    /// </summary>
    /// <param name="services">object service collection.</param>
    public static class DependicyInjection
    {
        /// <summary>
        /// method to add dependicy injection.
        /// </summary>
        /// <param name="services">IServicesCollection.</param>
        public static void AddDependicyInjection(this IServiceCollection services)
        {
            // user.
            services.AddTransient<IUserDao, UserDao>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserFacade, UserFacade>();

            services.AddTransient<IDatabaseContext, DatabaseContext>();
        }

        /// <summary>
        /// Method to add Db Context.
        /// </summary>
        /// <param name="services">IServiceCollection services.</param>
        /// <param name="logger">ILogger logger.</param>
        /// <param name="configuration">Configuration Options.</param>
        public static void AddDbContext(this IServiceCollection services, ILogger logger, IConfiguration configuration)
        {
            logger.Information("La cadena de conexion es {0}", configuration.GetConnectionString(nameof(DatabaseContext)));
            services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(DatabaseContext)));
            });
        }

        /// <summary>
        /// Add configuration Auto Mapper.
        /// </summary>
        /// <param name="services">IServiceCollection services.</param>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
            services.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}
