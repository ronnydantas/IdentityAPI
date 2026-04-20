using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityAPI.Extensions
{
    /// <summary>
    /// Configura toda a autenticação e validação JWT, incluindo integração com Swagger.
    /// </summary>
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                var jwtSecret = configuration["JWT:Secret"];
                if (string.IsNullOrEmpty(jwtSecret))
                {
                    throw new InvalidOperationException("A chave secreta JWT não está configurada. Verifique a configuração 'JWT:Secret'.");
                }
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),

                    ValidateLifetime = true
                };
            });

            return services;
        }
    }
}