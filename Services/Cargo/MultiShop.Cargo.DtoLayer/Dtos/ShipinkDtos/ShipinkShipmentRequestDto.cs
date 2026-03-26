using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkShipmentRequestDto
    {
        public string direction { get; set; } = "outgoing";
        public string order_id { get; set; } // Örn: "912e71c6...:18"
        public string carrier_service_id { get; set; } // "ptt_standart"
        public string carrier_account_id { get; set; }
        public string warehouse_id { get; set; }
        public string card_id { get; set; } // Kritik: "5d0e3a74..."
        public List<ShipinkPackageDto> packages { get; set; }
        public bool create_invoice { get; set; } = false;
    }
}
