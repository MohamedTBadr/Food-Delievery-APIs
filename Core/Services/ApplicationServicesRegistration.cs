using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;
using Shared.Authentication;


namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services , IConfiguration configuration)
        {

            Services.AddScoped<IServiceManager, ServicesManger>();
            Services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);


            Services.Configure<JWTOptions>(options=>configuration.GetSection("JWTOptions"));



            return Services;
        }
    }
}
