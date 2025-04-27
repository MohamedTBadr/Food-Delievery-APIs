using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.AuthenticationModule;
using Domain.Models.BasketModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ServicesAbstractions;
using Shared.Authentication;



namespace Services
{
    public class ServicesManger(IUnitOfWork unitOfWork , IMapper mapper, IBasketRepository BasketRepo,UserManager<ApplicationUser> UserManager, IOptions<JWTOptions> options) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyProductService
            = new Lazy<IProductService>(() => new ProductServices(unitOfWork, mapper));

        private readonly Lazy<IBasketService> LazyBasketService
            = new(() => new BasketService(BasketRepo, mapper));


        private readonly Lazy<IAuthenticationService> LazyAuthenticationService
            = new(() => new AuthenticationService(UserManager,options,mapper));


        private readonly Lazy<IOrderService> LazyOrderService = new(() => new OrderService(mapper, unitOfWork, BasketRepo));

        public IProductService ProductService => LazyProductService.Value ;

        public IBasketService BasketService => LazyBasketService.Value ;

      public  IAuthenticationService AuthenticationService => LazyAuthenticationService.Value ;

        public IOrderService OrderService => LazyOrderService.Value;
    }
}
