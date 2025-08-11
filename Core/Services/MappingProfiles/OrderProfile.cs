using AutoMapper;
using Domain.Models.OrderModule;
using Shared.Authentication;
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
                .ForMember(d => d.PictureUrl, o => o.MapFrom(n => n.Product.PictureUrl))
                .ForMember(d => d.Price, o => o.MapFrom(n => n.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(n => n.Quantity));

            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethodId)) 
                .ForMember(d => d.Total, o => o.MapFrom(s => s.DeliveryMethod != null ? s.DeliveryMethod.Price + s.Subtotal : s.Subtotal));

            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }
    }


}
