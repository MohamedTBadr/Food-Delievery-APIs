using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServicesManger>();
            Services.AddAutoMapper(typeof(Services.AssemblyRefrence).Assembly);

            return Services;
        }
    }
}
