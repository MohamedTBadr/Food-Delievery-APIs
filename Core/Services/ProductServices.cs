using AutoMapper;
using Domain.Contracts;
using Shared.DataTransferObject.Products;
using Services.Specifications;
using System;
using Shared.DataTransferObject;
using Domain.Exceptions;
using Domain.Models.ProductModule;
using ServicesAbstractions;
namespace Services
{
    internal class ProductServices(IUnitOfWork UnitOfWork , IMapper Mapper ) : IProductService
    {
        public async Task<PaginatedObject<ProductResponse>> GetAllProductAsync(ProductQueryParams parameters)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var Repo = UnitOfWork.GetRepository<Product,int>();
                
            var Products= await Repo.GetAllAsync(Specifications);


            var data= Mapper.Map<IEnumerable<Product>,IEnumerable<ProductResponse>>(Products);
            var PageCount = data.Count();
            var TotalCount = await UnitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(parameters));
            return new PaginatedObject<ProductResponse>(parameters.PageIndex,PageCount,TotalCount,data);

        }

        public async Task<ProductResponse> GetProductAsync(int id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(id);

            var Product = await  UnitOfWork.GetRepository<Product, int>().GetAsync(Specifications)
                ?? throw new ProductNotFoundException(id)
                ;


            return Mapper.Map<Product,ProductResponse>(Product);  


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
