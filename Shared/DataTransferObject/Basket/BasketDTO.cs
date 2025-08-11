using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Basket
{
    public record BasketDTO
    {
        public string Id { get; set; }

        public ICollection<basketItemDTo> BasketItems { get; set; }

    }
}
