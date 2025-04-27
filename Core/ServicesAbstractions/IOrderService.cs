using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;
using Shared.Orders;

namespace ServicesAbstractions
{
    public interface IOrderService
    {

        ///Create()
        Task<OrderResponse> CreateAsync(OrderRequest Request,string email);


        //Update
        Task<OrderResponse> GetAsync(Guid id);
        Task<IEnumerable<OrderResponse>> GetAllAsync(string email);
         Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    }
}
