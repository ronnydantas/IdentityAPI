using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Infrastructure.Context
{
    public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
    {
        public PostgresContext CreateDbContext(string[] args)
        {
            var current = Directory.GetCurrentDirectory();
            var startupPath = Path.GetFullPath(Path.Combine(current, "..", "IdentityAPI"));
            var basePath = Directory.Exists(startupPath) ? startupPath : current;

            var jsonPath = Path.Combine(basePath, "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(jsonPath, optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? configuration["ConnectionStrings:DefaultConnection"]
                                ?? Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found. Configure em appsettings.json ou via variável de ambiente.");

            var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new PostgresContext(optionsBuilder.Options);
        }
    }
}