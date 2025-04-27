using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.AuthenticationModule;

namespace Domain.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        //Id=>Guid
        public string UserEmail { get; set; }


        public DateTimeOffset DateTimeOffset { get; set; }=DateTimeOffset.Now;

        public IEnumerable<OrderItem> Items { get; set; }=new List<OrderItem>();

        public OrderAddress Address { get; set; }


        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }


        public PaymentStatus PaymentStatus { get; set; }

        public string PaymentIntentId {  get; set; }

        public decimal Subtotal {  get; set; }

        public Order()
        {
            
        }

        public Order(string userEmail,
            IEnumerable<OrderItem> items,
            OrderAddress address,
            DeliveryMethod deliveryMethod, 
            decimal subtotal)
        {
            UserEmail = userEmail;
            Items = items;
            Address = address;
            DeliveryMethod = deliveryMethod;
            //PaymentIntentId = paymentIntentId;
            Subtotal = subtotal;
        }
    }

}
