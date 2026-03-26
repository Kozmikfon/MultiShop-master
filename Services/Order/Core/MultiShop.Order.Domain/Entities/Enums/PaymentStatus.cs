using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.Entities.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,    // Beklemede
        Completed = 1,  // Tamamlandı
        Failed = 2,     // Hatalı
        Refunded = 3    // İade Edildi
    }
}
