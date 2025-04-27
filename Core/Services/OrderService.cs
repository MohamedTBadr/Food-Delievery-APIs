using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Services.Specifications;
using ServicesAbstractions;
using Shared.Orders;

namespace Services
{
    public class OrderService(IMapper mapper,IUnitOfWork unitOfWork,IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResponse> CreateAsync(OrderRequest Request, string email)
        {
            var Basket=await basketRepository.GetbasketsAsync(Request.BasketId)??throw new BasketNotFoundException(Request.BasketId);
            var address = mapper.Map<OrderAddress>(Request.Address);
            List<OrderItem> items = new List<OrderItem>();

            var ProductRepo = unitOfWork.GetRepository<Product>();
            foreach (var item in Basket.Items)
            {

                var Product = await ProductRepo.GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);


                items.Add(CreateOrderItem(Product,item));
                item.ProductPrice = Product.Price;
                
            }

            var DeliveryMethod=await unitOfWork.GetRepository<DeliveryMethod>().GetAsync(Request.DeliveryMethodId)??
                throw new DeliveryMethodNotFoundException(Request.DeliveryMethodId);


            var subtotal = items.Sum(p=>p.Price * p.Quantity);
            var order=new Order(email,items,address,DeliveryMethod,subtotal);

            unitOfWork.GetRepository<Order,Guid>().Add(order);
          await  unitOfWork.saveChangesAsync();
            

            return mapper.Map<OrderResponse>(order);


        }

        private static OrderItem CreateOrderItem(Product product, BasketItems item)
        => new(new(product.Id, product.Name, product.PictureUrl), product.Price, item.Quantity);

        public async Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecifications(email));

            return mapper.Map<IEnumerable<OrderResponse>>(orders);   

        }

        public async Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            var DeliveryMethods= await unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>>(DeliveryMethods);
        }

        public async Task<OrderResponse> GetAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderSpecifications(id));

            return mapper.Map<OrderResponse>(order);
        }
    }
}
