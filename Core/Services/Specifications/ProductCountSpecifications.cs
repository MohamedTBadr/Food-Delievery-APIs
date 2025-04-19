using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared.DataTransferObject.Products;

namespace Services.Specifications
{
    internal class ProductCountSpecifications(ProductQueryParams Params)
        : BaseSpecifications<Product>(CreateCriteria(Params))
    {
        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParams Params)
        {
            return product =>
                        (!Params.BrandId.HasValue || product.ProductBrandId == Params.BrandId.Value)
                        &&
                        (!Params.TypeId.HasValue || product.ProductTypeId == Params.TypeId.Value)
                        &&
                        (string.IsNullOrWhiteSpace(Params.Search) || product.Name.ToLower().Contains(Params.Search.ToLower()))
                        ;
        }

    }
}
