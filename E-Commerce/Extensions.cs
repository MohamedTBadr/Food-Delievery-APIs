using Domain.Contracts;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }



        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIResponse;

            });
            services.AddSwaggerServices();
            return services;
        }

        public static async Task<WebApplication> IntializeDatabaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();

            var DbIntialize = Scope.ServiceProvider.GetRequiredService<IDbIntialize>();
            await DbIntialize.IntializeAsync();
            return app;
        }
    }
}
