using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkShipmentResponseDto
    {
        public bool success { get; set; }

        public string message { get; set; }
        public ShipinkDataDto data { get; set; }
    }

}
