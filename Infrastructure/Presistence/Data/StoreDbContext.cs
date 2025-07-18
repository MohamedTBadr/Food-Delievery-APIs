﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType>ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
