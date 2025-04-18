using Shared.DataTransferObject.Products;


namespace Domain.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductAsync(int? brandId ,int? typeId,ProductSortingOptions options);


        Task<ProductResponse> GetProductAsync(int id);

        Task<IEnumerable<BrandResponse>> GetBrandsAsync();


        Task<IEnumerable<TypeResponse>> GetTypeAsync();

    }
}
