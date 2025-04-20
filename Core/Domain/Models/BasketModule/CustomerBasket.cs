using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModule
{
    public class CustomerBasket
    {
        public string BasketId {  get; set; }  //Create from client 

        public ICollection<BasketItems> Items { get; set; }

    }
}
