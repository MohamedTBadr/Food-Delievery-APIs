using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransferObject.Basket;

namespace Services.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDTO>().ReverseMap();

            CreateMap<BasketItems,BasketDTO>().ReverseMap();
        }
    }
}
