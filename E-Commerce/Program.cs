using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence;
using System.Threading.Tasks;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
                {
                    var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                    options.UseSqlServer(ConnectionString);

                });
            builder.Services.AddScoped<IDbIntialize, DbIntialize>();

            var app = builder.Build();
            await IntialDbAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    public static async Task IntialDbAsync(WebApplication app)
        {
            using var Scope = app.Services.CreateScope();

            var DbIntialize = Scope.ServiceProvider.GetRequiredService<IDbIntialize>();
            await DbIntialize.IntializeAsync();
        }
    }
}
