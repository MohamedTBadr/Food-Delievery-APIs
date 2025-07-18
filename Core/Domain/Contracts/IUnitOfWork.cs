﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> saveChangesAsync();


        IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>()
            where TEntity : BaseEntity<TKey>; 
        IGenericRepository<TEntity,int> GetRepository<TEntity>()
            where TEntity : BaseEntity<int>;
    }
}
