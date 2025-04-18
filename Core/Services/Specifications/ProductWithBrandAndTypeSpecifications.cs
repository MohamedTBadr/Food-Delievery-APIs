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
        public ProductWithBrandAndTypeSpecifications(int? brandId,int?TypeId,ProductSortingOptions options)
            :base(product=>
            (!brandId.HasValue || product.ProductBrandId==brandId.Value )
            &&
            (!TypeId.HasValue || product.ProductTypeId == TypeId.Value)
            )
        {


            //Add Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p=>p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p=>p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p=>p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p=>p.Price);
                    break;
                default:
                    break;
            }


        }
    }
}
