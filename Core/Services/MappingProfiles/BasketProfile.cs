using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransferObject.Basket;

namespace Services.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.BasketId))
                .ForMember(d => d.BasketItems, opt => opt.MapFrom(s => s.Items));

            CreateMap<BasketDTO, CustomerBasket>()
                .ForMember(d => d.BasketId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Items, opt => opt.MapFrom(s => s.BasketItems));

            CreateMap<BasketItems, basketItemDTo>()
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.ProductPrice));

            CreateMap<basketItemDTo, BasketItems>()
                .ForMember(d => d.ProductPrice, opt => opt.MapFrom(s => s.Price));
        }
    }
}
