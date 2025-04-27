using System.Text;
using Domain.Contracts;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Authentication;

namespace E_Commerce
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }



        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIResponse;

            });
            services.AddSwaggerServices();

            ConfigureJWT(services,configuration);
            return services;
        }

        public static async Task<WebApplication> IntializeDatabaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();

            var DbIntialize = Scope.ServiceProvider.GetRequiredService<IDbIntialize>();
            await DbIntialize.IntializeAsync();
            return app;
        }

        private static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme; ;
                config.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))
                };
            });
        }
    }
}
