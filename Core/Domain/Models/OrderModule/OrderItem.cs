using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.OrderModule
{
    public class OrderItem:BaseEntity<Guid>
    {
        public OrderItem(ProductInOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public OrderItem()
        {
            
        }
        public ProductInOrder Product { get; set; }
        public decimal Price { get; set; }


        public int Quantity {  get; set; }


    }

}
