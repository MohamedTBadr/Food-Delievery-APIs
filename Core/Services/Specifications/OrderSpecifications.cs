using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;

namespace Services.Specifications
{
    internal class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(Guid id) : base(order=>order.Id==id)
        {
            AddInclude(x=>x.DeliveryMethod);
            AddInclude(x=>x.Items);
        }    
        public OrderSpecifications(string email) : base(order=>order.UserEmail==email)
        {

            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);

            AddOrderByDesc(x=>x.DateTimeOffset);
        }



    }
}
