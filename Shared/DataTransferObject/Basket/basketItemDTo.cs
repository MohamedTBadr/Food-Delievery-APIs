using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.Basket
{
    public record basketItemDTo
    {
        public string Id { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        [Range(1,int.MaxValue)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
