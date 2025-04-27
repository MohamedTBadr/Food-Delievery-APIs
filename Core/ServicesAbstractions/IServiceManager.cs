using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;

namespace ServicesAbstractions
{
    public interface IServiceManager
    {
        public IProductService ProductService { get;}
        public IBasketService BasketService { get;}
        IAuthenticationService AuthenticationService { get;}

        public IOrderService OrderService { get;}

    }
}
