using MultiShop.Order.Domain.Entities.Enums;
using MultiShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.Entities
{
    public class Ordering
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public double TotalWeight { get; set; }
        public DateTime OrderDate { get; set; }
        public int AddressId { get; set; }
        public string ShipinkOrderId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public List<OrderDetail> OrderDetails { get; set; }

        public string? TrackingNumber { get; set; } // Kargo takip numarası (isteğe bağlı)
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending; // Sipariş durumu (örneğin: Pending, Shipped, Delivered)
        
    }
}
