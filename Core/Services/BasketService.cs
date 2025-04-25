using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServicesAbstractions;
using Shared.DataTransferObject.Basket;

namespace Services
{
    internal class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteAsync(string id)
        {
            return await basketRepository.DeleteBasketAsync(id);
        }

        public async Task<BasketDTO> GetAsync(string id)
        {

           var basket = await basketRepository.GetbasketsAsync(id) ??
            throw new BasketNotFoundException(id);
                
            return mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateAsync(BasketDTO basket)
        {
                var customerBasket= mapper.Map<CustomerBasket>(basket);

                var UpdatedBasket=await basketRepository.UpdateBasketAsync(customerBasket)?? throw new Exception("can't perform process on basket now");

            return mapper.Map<BasketDTO>(UpdatedBasket);

        }
    }
}
