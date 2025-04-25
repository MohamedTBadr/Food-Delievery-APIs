using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Products;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IServiceManager serviceManger):ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult<PaginatedObject<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParams Params)
        {
            var Products= await serviceManger.ProductService.GetAllProductAsync( Params);
            return Ok(Products);
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            var Product= await serviceManger.ProductService.GetProductAsync(id);
            return Ok(Product);
        }


         [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands()
        {
            var Brands= await serviceManger.ProductService.GetBrandsAsync();
            return Ok(Brands);
        }

        
         [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes()
        {
            var Types= await serviceManger.ProductService.GetTypeAsync();
            return Ok(Types);
        }






    }
}
