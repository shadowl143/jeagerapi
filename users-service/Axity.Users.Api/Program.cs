// <summary>
// <copyright file="Program.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Api
{
    using Axity.Users.Api;
    using Axity.Users.Entities.Context;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// program.
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();

            // Cargar configuraci�n desde Startup
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            // Automáticamente crear la base de datos / aplicar migraciones:
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
            }

            startup.Configure(app);

            app.Run();
        }
    }
}