namespace Domain.Models.OrderModule
{
    public class ProductInOrder
    {
        public ProductInOrder(int id, string productName, string pictureUrl)
        {
            Id = id;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }
        public ProductInOrder()
        {
            
        }
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

    }

}
