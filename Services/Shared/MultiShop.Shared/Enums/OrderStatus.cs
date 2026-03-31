using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Enums
{
    public enum OrderStatus
    {
        Pending = 0,    // Beklemede
        Completed = 1,  // Ödemesi Alındı / Onaylandı
        Shipped = 2,    // Kargoya Verildi
        Delivered = 3,  // Teslim Edildi
        Cancelled = 4   // İptal Edildi
    }
}
