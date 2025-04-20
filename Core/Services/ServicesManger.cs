using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.BasketModule;
using ServicesAbstractions;

namespace Services
{
    public class ServicesManger(IUnitOfWork unitOfWork , IMapper mapper, Domain.Contracts.IBasketService BasketRepo) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyProductService
            = new Lazy<IProductService>(() => new ProductServices(unitOfWork, mapper));

        private readonly Lazy<ServicesAbstractions.IBasketService> LazyBasketService
            = new Lazy<ServicesAbstractions.IBasketService>(() => new BasketService(BasketRepo, mapper));

        public IProductService ProductService => LazyProductService.Value ;

        public IBasketService BasketService => LazyBasketService.Value ;
    }
}
