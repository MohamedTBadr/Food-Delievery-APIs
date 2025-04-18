using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity> : ISpecifications<TEntity> where TEntity : class
    {
        
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;

        }
        public Expression<Func<TEntity, bool>> Criteria {  get; private set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        Expression<Func<TEntity, object>> OrderBy { get; private set; }
        
        Expression<Func<TEntity, object>> OrderByDesc
  { get; private set; }


        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpressions.Add(include);
        }

        protected void AddOrderBy(Expression<Func<TEntity,Object>> orderby)
        {
            OrderBy = orderby;

        }protected void AddOrderByDesc(Expression<Func<TEntity,Object>> orderbyDesc)
        {
            OrderByDesc = orderbyDesc;

        }
    }
}
