using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Authentication;

namespace Shared.Orders
{
    public record OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }


        public DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.Now;

        public IEnumerable<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();

        public AddressDTO Address { get; set; }


        public int DeliveryMethod { get; set; }


        public string PaymentStatus { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

    }

    public record OrderItemDTO
    {
        public string ProductName {  get; set; }
        public string PictureUrl {  get; set; }

        public decimal Price { get; set; }


        public int Quantity { get; set; }

    }
}
