using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Repository;

namespace Presistence
{
    public static class InfraStructureServiceRegistrations
    {

        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services ,IConfiguration configuration)
        {
           Services.AddDbContext<StoreDbContext>(options =>
            {
                var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(ConnectionString);

            });

            Services.AddScoped<IDbIntialize, DbIntialize>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
