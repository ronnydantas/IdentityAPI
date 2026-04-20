using Identity.Domain;
using Identity.Infrastructure;
using Identity.Infrastructure.Context;
using IdentityAPI.Extensions;
using IdentityAPI.Extensions.SwaggerConfigurations;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Classe principal do aplicativo Cliente API.
/// </summary>
public class Program
{
    /// <summary>
    /// Ponto de entrada principal do aplicativo.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração de serviços
        builder.Services
            .AddSwaggerConfig(builder.Configuration)
            .AddControllers();

        builder.Services.AddCustomCors();

        builder.Services.AddRepository(builder.Configuration);
        builder.Services.AddService(builder.Configuration);

        // Adiciona configuração JWT centralizada
        builder.Services.AddJwtAuthentication(builder.Configuration);

        // Exemplo: em Program.cs ou na extensão que registra o DbContext
        builder.Services.AddDbContext<PostgresContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                              b => b.MigrationsAssembly("Identity.Infrastructure")));

        var app = builder.Build();

        // Ajuste do caminho base para o nome correto do projeto
        app.UsePathBase("/identity-api");

        app.UseCustomCors();

        app.UseRouting();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/identity-api/swagger/v1/swagger.json", "Identity API V1");
            options.RoutePrefix = string.Empty;
        });

        // Adiciona autenticação e autorização JWT
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}