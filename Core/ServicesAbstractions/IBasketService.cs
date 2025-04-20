using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject.Basket;

namespace ServicesAbstractions
{
    public interface IBasketService
    {
        Task<BasketDTO > GetAsync(string id);
        Task<BasketDTO > UpdateAsync(BasketDTO basket);
        Task<bool> DeleteAsync(string id);



    }
}
