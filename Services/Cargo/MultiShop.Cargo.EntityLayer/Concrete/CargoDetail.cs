using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoDetail
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; } 
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
        public int CargoCustomerId { get; set; }
        public CargoCustomer CargoCustomer { get; set; }
        public string VendorId { get; set; }
        public int OrderingId { get; set; }

        // yeni eklenen alanlar
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverDistrict { get; set; }
        public string ReceiverAddressDetail { get; set; }
        public string? TrackingNumber { get; set; }
        public string? ShipinkOrderId { get; set; }
        public string? ShipinkShipmentId { get; set; }

        public double Weight { get; set; } // Ağırlık (kg)
        public int Width { get; set; }   // Genişlik (cm)
        public int Height { get; set; }  // Yükseklik (cm)
        public int Length { get; set; }  // Uzunluk (cm)

        public CargoStatus CurrentStatus { get; set; }
    }
}
