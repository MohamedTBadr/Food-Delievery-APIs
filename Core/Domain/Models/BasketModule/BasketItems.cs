namespace Domain.Models.BasketModule
{
    public class BasketItems
    {
        public int Id { get; set; } //Product ID

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string PictureUrl { get; set; }
        public int Quantity {  get; set; }


    }
}
