using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Identity.Infrastructure.Context
{
    public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
    {
        public PostgresContext CreateDbContext(string[] args)
        {
            // Tenta carregar appsettings.json do projeto de inicialização (IdentityAPI) ou do diretório atual
            var current = Directory.GetCurrentDirectory();
            var startupPath = Path.GetFullPath(Path.Combine(current, "..", "IdentityAPI"));
            var basePath = Directory.Exists(startupPath) ? startupPath : current;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
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