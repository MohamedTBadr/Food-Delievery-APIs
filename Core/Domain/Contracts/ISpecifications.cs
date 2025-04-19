using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;

    public interface ISpecifications<TEntity> where TEntity : class
    {
        Expression<Func<TEntity,bool>> Criteria { get;}

        List<Expression<Func<TEntity,object>>> IncludeExpressions { get;}
    Expression<Func<TEntity, Object>> OrderBy { get;  }
    Expression<Func<TEntity, Object>> OrderByDesc { get; }

    public int Skip { get; }
    public int Take { get; }

    public bool IsPaginated { get; }

    }
//where =>Criteria Expression

