using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence;
using System.Threading.Tasks;
using Presistence.Repository;
using Services;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using E_Commerce.Factories;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
          
          

            var app = builder.Build();
            await app.IntializeDatabaseAsync();
            app.UseCustomExceptionHandlerMiddleWare();            //app.Use(async(context, next) =>
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
   
    }
}
