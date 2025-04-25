using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Authentication;
using Presistence.Data;
using Presistence.Repository;
using StackExchange.Redis;

namespace Presistence
{
    public static class InfraStructureServiceRegistrations
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                var ConnectionString = configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(ConnectionString);
            });


            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var ConnectionString = configuration.GetConnectionString("IdentityConnection");

                options.UseSqlServer(ConnectionString);
            });


            Services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>((_) =>
            {
              return  ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });

             
            Services.AddScoped<IDbIntialize, DbIntialize>();

            Services.AddScoped<IBasketRepository,CustomerbasketRepository>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
