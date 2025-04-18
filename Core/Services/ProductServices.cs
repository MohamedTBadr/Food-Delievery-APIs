using AutoMapper;
using Domain.Contracts;
using Shared.DataTransferObject.Products;
using Domain.Models;
using Services.Specifications;
using System;
namespace Services
{
    internal class ProductServices(IUnitOfWork UnitOfWork , IMapper Mapper ) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetAllProductAsync(int? brandId, int? typeId,ProductSortingOptions options)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(brandId ,typeId,options);
            var Repo = UnitOfWork.GetRepository<Product,int>();
                
            var Products= await Repo.GetAllAsync(Specifications);


            return Mapper.Map<IEnumerable<Product>,IEnumerable<ProductResponse>>(Products);

        }

        public async Task<ProductResponse> GetProductAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);

            var Repo = await  UnitOfWork.GetRepository<Product, int>().GetAsync(Specifications);


            return Mapper.Map<Product,ProductResponse>(Repo);  


        }

        public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
        {
            var Repo = UnitOfWork.GetRepository<ProductBrand,int>();

            var Brands= await Repo.GetAllAsync();

            return Mapper.Map<IEnumerable<ProductBrand> , IEnumerable<BrandResponse>>(Brands);
            
        }

        public async Task<IEnumerable<TypeResponse>> GetTypeAsync()
        {
            var Repo = UnitOfWork.GetRepository<ProductType, int>();
            var Types= await Repo.GetAllAsync();

            return Mapper.Map<IEnumerable<ProductType> , IEnumerable<TypeResponse>>(Types);
             
        }
    }
}
