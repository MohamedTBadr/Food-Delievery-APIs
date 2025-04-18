using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Shared.DataTransferObject.Products;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile
    {

        public ProductProfile()
        {
            CreateMap<Product,ProductResponse>()
                .ForMember(d=>d.ProductBrand ,
                options=>options.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType ,
                options=>options.MapFrom(s=>s.ProductType.Name))

                .ForMember(d=>d.PictureUrl,options=>options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductBrand, BrandResponse>();


            CreateMap<ProductType, TypeResponse>();
        }
    }
}

internal class PictureUrlResolver : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrWhiteSpace(source.PictureUrl))
        {
            return $"https://localhost.7104/{source.PictureUrl}";
        }
        return "";
    }
}
