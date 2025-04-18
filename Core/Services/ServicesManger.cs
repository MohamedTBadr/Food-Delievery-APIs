using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;

namespace Services
{
    public class ServicesManger(IUnitOfWork unitOfWork , IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> LazyProductService
            = new Lazy<IProductService>(() => new ProductServices(unitOfWork, mapper));

        public IProductService ProductService => LazyProductService.Value ;
    }
}
