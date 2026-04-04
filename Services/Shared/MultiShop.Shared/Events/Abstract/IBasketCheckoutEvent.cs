using MultiShop.Shared.Events.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Abstract
{
    public interface IBasketCheckoutEvent
    {
        public string UserId { get; set; }
        public string Name { get; set; } // Ödeme anında alınacak
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public double TotalWeight { get; set; } // 🚀 İşte o meşhur hesapladığımız ağırlık!
        public List<BasketItemEvent> BasketItems { get; set; }
    }
}
