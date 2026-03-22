using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkShipmentResponseDto
    {
        public string id { get; set; } // Gönderi ID'si
        public CarrierResult carrier { get; set; }
    }
}
