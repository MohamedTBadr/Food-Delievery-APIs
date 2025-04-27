using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.Authentication;
using Shared.DataTransferObject.Products;
using Shared.Orders;

namespace Services.MappingProfiles
{
    public class OrderProfile:Profile
    {

        public OrderProfile()
        {
            CreateMap<OrderAddress,AddressDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductName, o => o.MapFrom(n => n.Product.ProductName))
                 .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderitemPictureUrlResolver>());



            CreateMap<Order, OrderResponse>().ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod.ShortName))
                .ForMember(d=>d.Total,o=>o.MapFrom(s=>s.DeliveryMethod.Price + s.Subtotal));


            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }

    }

    internal class OrderitemPictureUrlResolver(IConfiguration configuration) 
        : IValueResolver<OrderItem, OrderItemDTO, string>
    {


        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.Product.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
            }
            return "";
        }
    }
}
