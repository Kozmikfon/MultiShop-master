using MultiShop.Shared.Events.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Concrete
{
    public class OrderCreatedEvent :IOrderCreatedEvent
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverDistrict { get; set; }
        public string ReceiverAddressDetail { get; set; }
        public int CargoCompanyId { get; set; }
        public int CargoCustomerId { get; set; }
        public string VendorId { get; set; }
        public string SenderCustomer { get; set; }

        // 🚀 İşte o meşhur fiziksel veriler buraya akacak:
        public double Weight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public string ShipinkOrderId { get; set; }
    }
}
