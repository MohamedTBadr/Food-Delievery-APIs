using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistence.Authentication;
using Presistence.Data;

namespace Presistence
{
    public class DbIntialize(StoreDbContext context,StoreIdentityDbContext IdentityContext) : IDbIntialize
    {
        public async Task IntializeAsync()
        {
            //production =>Seeding + Intialize Db
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
               await context.Database.MigrateAsync();
            }

            //Dev =>Seeding
            try { 
            if (!context.Set<ProductBrand>().Any())
            {
                var data = await File.ReadAllTextAsync(@"../Infrastructure\Presistence\Seeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                if (brands is not null && brands.Any())
                {
                    context.Set<ProductBrand>().AddRange(brands);
                    await context.SaveChangesAsync();
                }
            }


            if (!context.Set<ProductType>().Any())
            {
                var data = await File.ReadAllTextAsync(@"../Infrastructure\Presistence\Seeding\types.json");

                var Types = JsonSerializer.Deserialize<List<ProductType>>(data);

                if (Types is not null && Types.Any())
                {
                    context.Set<ProductType>().AddRange(Types);
                    await context.SaveChangesAsync();
                }
            }



            if (!context.Set<Product>().Any())
            {
                var data = await File.ReadAllTextAsync(@"../Infrastructure\Presistence\Seeding\products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(data);

                if (Products is not null && Products.Any())
                {
                    context.Set<Product>().AddRange(Products);
                    await context.SaveChangesAsync();
                }
            }




                if (!context.Set<DeliveryMethod>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"../Infrastructure\Presistence\Seeding\delivery.json");

                    var Types = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);

                    if (Types is not null && Types.Any())
                    {
                        context.Set<DeliveryMethod>().AddRange(Types);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception E){
                Console.WriteLine($"Error Occurred during seeding: {E.Message}");
            }

        }

     public async  Task  IntializeIdentityAsync()
        {
            if ((await IdentityContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await IdentityContext.Database.MigrateAsync();
            }




        }
    }
}
