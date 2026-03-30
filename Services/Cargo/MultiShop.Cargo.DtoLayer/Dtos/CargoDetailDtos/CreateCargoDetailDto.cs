using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos
{
    public class CreateCargoDetailDto
    {
        // Shipink 'customer' ve 'address' verilerinden gelenler
        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; } // Hata alıyordun, eklendi!
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverAddressDetail { get; set; } // Hata alıyordun, eklendi!
        public string ReceiverCity { get; set; }
        public string ReceiverDistrict { get; set; }
        public int CargoCompanyId { get; set; }
        public int CargoCustomerId { get; set; }

        // Shipink '/shipments' (Sevkiyat) için hayati olan UUID'ler
        public string ShipinkOrderId { get; set; } // order_id
        public string CarrierAccountId { get; set; } // cb333299...
        public string WarehouseId { get; set; } // 7ad6e1be...
        public string CarrierServiceId { get; set; } // ptt_standart

        // Teknik alanlar
        public string Barcode { get; set; }
        public string VendorId { get; set; }
        public int OrderingId { get; set; }
        public string SenderCustomer { get; set; }

        public double Weight { get; set; } // Ağırlık (kg)
        public int Width { get; set; }    // Genişlik (cm)
        public int Height { get; set; }   // Yükseklik (cm)
        public int Length { get; set; }   // Uzunluk (cm)
    }
}
