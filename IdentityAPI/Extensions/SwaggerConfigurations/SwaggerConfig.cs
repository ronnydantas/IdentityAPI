using Microsoft.OpenApi.Models;
using System.Reflection;

namespace IdentityAPI.Extensions.SwaggerConfigurations;

/// <summary>
/// Classe responsável pela configuração do Swagger na aplicação.
/// </summary>
internal static class SwaggerConfig
{
    /// <summary>
    /// Adiciona e configura o Swagger no container de injeção de dependência.
    /// </summary>
    internal static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            // Filtros
            options.OperationFilter<SwaggerDefaltValues>();
            options.OperationFilter<ApiVersionFilter>();

            var executingAssembly = Assembly.GetExecutingAssembly();

            var xmlFile = $"{executingAssembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            options.IncludeXmlComments(xmlPath);

            var referencedProjectsXmlDocPaths = executingAssembly.GetReferencedAssemblies()
                .Where(a => a.Name != null && a.Name.StartsWith("IdentityAPI", StringComparison.InvariantCultureIgnoreCase))
                .Select(a => Path.Combine(AppContext.BaseDirectory, $"{a.Name}.xml"));

            foreach (var xmlDocPath in referencedProjectsXmlDocPaths)
            {
                if (File.Exists(xmlDocPath))
                    options.IncludeXmlComments(xmlDocPath);
            }

            // 🔐 JWT Bearer
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Digite: Bearer {seu token JWT}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services; // 🔥 faltava isso
    }
}