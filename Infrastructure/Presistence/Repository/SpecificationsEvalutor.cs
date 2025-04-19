using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Repository
{
    public static class SpecificationsEvalutor
    {

        public static IQueryable<TEntity> CreateQuery<TEntity>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity> specifications) where TEntity : class
        {
            var Query = InputQuery;
            if(specifications.Criteria is not null)
            {
                Query= Query.Where(specifications.Criteria);
            }
            foreach (var spec in specifications.IncludeExpressions)
            {
                //rkzzzz hna el awla mshtghltsh
                Query= specifications.IncludeExpressions.Aggregate(Query,(currentQuery,include)=>
                currentQuery.Include(include)
                );
            }

            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDesc is not null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDesc);
            }

            if(specifications.IsPaginated)
            {
                Query=Query.Skip(specifications.Skip).Take(specifications.Take);
            }
                return Query;
        } 
    }
}
//set of tentity,specifications T