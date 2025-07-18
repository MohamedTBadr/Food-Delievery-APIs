﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repository
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext Context)
        : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetAsync(TKey Key)
        =>
            await Context.Set<TEntity>().FindAsync(Key);
        

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        =>
            await Context.Set<TEntity>().ToListAsync();
        

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        async Task<TEntity> IGenericRepository<TEntity, TKey>.GetAsync(ISpecifications<TEntity> specifications)
          =>
          await SpecificationsEvalutor.CreateQuery(Context.Set<TEntity>(), specifications).FirstOrDefaultAsync();



        async Task<IEnumerable<TEntity>> IGenericRepository<TEntity, TKey>.GetAllAsync(ISpecifications<TEntity> specifications)
         =>
             await SpecificationsEvalutor.CreateQuery(Context.Set<TEntity>(), specifications).ToListAsync();

         async Task<int> IGenericRepository<TEntity, TKey>.CountAsync(ISpecifications<TEntity> specifications)
        {
            return await SpecificationsEvalutor.CreateQuery(Context.Set<TEntity>(), specifications).CountAsync();
        }
    }
}
