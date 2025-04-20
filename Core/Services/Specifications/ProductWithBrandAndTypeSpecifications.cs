using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.Products;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        //use to get product by id
        public ProductWithBrandAndTypeSpecifications(int id) : base(product=>product.Id==id)
        {

            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p=>p.ProductType);

        }

        //all prpduct
        //use sorting & filtration
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams Params)
            : base(CreateCriteria(Params)
            )
        {


            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplySorting(Params.Options);

            ApplyPagination(Params.PageSize, Params.PageIndex);

        }
        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParams Params)
        {
            return product =>
                        (!Params.BrandId.HasValue || product.ProductBrandId == Params.BrandId.Value)
                        &&
                        (!Params.TypeId.HasValue || product.ProductTypeId == Params.TypeId.Value)
                        &&
                        (string.IsNullOrWhiteSpace(Params.Search)|| product.Name.ToLower().Contains(Params.Search.ToLower()))
                        ;
        }

        private void ApplySorting(ProductSortingOptions options)
        {
            switch (options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;
            }
        }



    }
}
