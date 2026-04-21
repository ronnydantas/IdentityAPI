using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Infrastructure;
using Identity.Infrastructure.Context;
using IdentityAPI.Extensions;
using IdentityAPI.Extensions.SwaggerConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using MediatR;
using System.Reflection;

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

        // Em Program.cs (antes de registrar seus services que dependem de Identity)
        builder.Services.AddDbContext<PostgresContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                              b => b.MigrationsAssembly("Identity.Infrastructure")));

        // Registrar Identity (isso adiciona SignInManager e UserManager)
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<PostgresContext>()
            .AddDefaultTokenProviders();

        // Depois registre seus repositórios/serviços que dependem de SignInManager/UserManager
        builder.Services.AddRepository(builder.Configuration);
        builder.Services.AddService(builder.Configuration);

        // Registrar MediatR handlers
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        // Registrar autenticação JWT (separado)
        builder.Services.AddJwtAuthentication(builder.Configuration);

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