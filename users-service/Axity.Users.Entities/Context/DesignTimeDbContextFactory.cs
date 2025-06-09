

namespace Axity.Users.Entities.Context
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Factory for creating DatabaseContext at design time (for EF Core migrations).
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            // Asumo que estás corriendo Add-Migration desde el proyecto Entities.Context
            // Y el appsettings.json está en la API:
            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddEnvironmentVariables()
            .Build();

            foreach (var kvp in configuration.AsEnumerable())
            {
                Console.WriteLine($"{kvp.Key} = {kvp.Value}");
            }

            var connectionString = configuration.GetConnectionString("ConnectionStrings:DatabaseContext"); // método correcto
            Console.WriteLine($"Desde configuration.GetConnectionString(\"DatabaseContext\"): {connectionString}");

            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
