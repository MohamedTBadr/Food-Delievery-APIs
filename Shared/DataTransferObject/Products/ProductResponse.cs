using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Products
{
    //record=> ref type compare by Value
    //property get , Init
    public record ProductResponse
    {
        public int Id { get; init; }

        public string Name { get; init; }
        public string Description { get; init; }

        public decimal Price { get; init; }

        public string PictureUrl {  get; init; }
        public string ProductBrand { get; init; }
        public string ProductType { get; init; }

    }
}
