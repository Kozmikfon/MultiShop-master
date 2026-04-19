using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Concrete
{
    public class ProductCreatedEvent
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
