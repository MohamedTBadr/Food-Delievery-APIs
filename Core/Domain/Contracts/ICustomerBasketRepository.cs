using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.BasketModule;
using Shared.DataTransferObject.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetbasketsAsync(string id);


        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket,TimeSpan? TimeToLive=null);

        Task<bool> DeleteBasketAsync(string id);


    }
}
