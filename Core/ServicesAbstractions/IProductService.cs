using Shared.DataTransferObject;
using Shared.DataTransferObject.Products;


namespace ServicesAbstractions
{
    public interface IProductService
    {
        Task<PaginatedObject<ProductResponse>> GetAllProductAsync(ProductQueryParams Params);


        Task<ProductResponse> GetProductAsync(int id);

        Task<IEnumerable<BrandResponse>> GetBrandsAsync();


        Task<IEnumerable<TypeResponse>> GetTypeAsync();

    }
}
