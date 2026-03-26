using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkDataDto
    {
        public string id { get; set; } // Shipink'teki gönderi ID'si
        public string orderId { get; set; } // Sipariş ID'si
        public CarrierResult carrier { get; set; } // Takip no burada
    }
}
