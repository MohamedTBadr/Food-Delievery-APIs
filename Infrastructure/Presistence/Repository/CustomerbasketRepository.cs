using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.BasketModule;
using ServicesAbstractions;
using StackExchange.Redis;

namespace Presistence.Repository
{
    public class CustomerbasketRepository(IConnectionMultiplexer multiplexer) : IBasketRepository
    {
        private readonly IDatabase _Database=multiplexer.GetDatabase();

        public async Task<bool> DeleteBasketAsync(string id)
        {
               return await   _Database.KeyDeleteAsync(id);

        }

        public async Task<CustomerBasket?> GetbasketsAsync(string id)
        {
            //Get Obj from Db
            // Deserialize
            //return
            var Basket = await _Database.StringGetAsync(id);

            if(Basket.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(Basket!);

        }

        //used for create and update
        public  async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {

                var Jsonbasket=JsonSerializer.Serialize(basket);

            var IsCreatedOrUpdated=     await   _Database.StringSetAsync(basket.BasketId, Jsonbasket,TimeToLive??TimeSpan.FromDays(7));
            return IsCreatedOrUpdated ?await GetbasketsAsync(basket.BasketId) : null;
        }
    }
}
