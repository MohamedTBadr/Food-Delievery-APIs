using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Orders;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager service) : APIController
    {

        ////Create(address,BasketId,DelieveryMethodId)  =>OrderResponse 
        [HttpPost]
       public  async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
        {
            return Ok(await service.OrderService.CreateAsync(request, GetEmailFromToken()));
        }



        ///GetDeliveryMethods
        [HttpGet("DeliveryMethods")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetAllDeliveryMethods()
        {
            return Ok(await service.OrderService.GetAllDeliveryMethodsAsync());
        }
        ///Get
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(Guid id)
        {
            return Ok(await service.OrderService.GetAsync(id));
        }

        ///GetAll   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllOrder()
        {
            return Ok(await service.OrderService.GetAllAsync(GetEmailFromToken()));
        }

    }
}
