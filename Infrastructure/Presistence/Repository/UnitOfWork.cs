using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Presistence.Data;

namespace Presistence.Repository
{
    public class UnitOfWork(StoreDbContext Context) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories=[];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName=typeof(TEntity).Name;

            if (_repositories.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity,TKey>) _repositories[TypeName];

            }
               var New= new GenericRepository<TEntity, TKey>(Context);
            _repositories.Add(TypeName, New);
            return New;

        }

        public IGenericRepository<TEntity, int> GetRepository<TEntity>() where TEntity : BaseEntity<int>
        {
            var TypeName = typeof(TEntity).Name;

            if (_repositories.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity, int>)_repositories[TypeName];

            }
            var New = new GenericRepository<TEntity, int>(Context);
            _repositories.Add(TypeName, New);
            return New;


        }

        public Task<int> saveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
