using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkCustomerDto
    {
        public string name { get; set; }
        public ShipinkContactDto email { get; set; }
        public ShipinkContactDto phone { get; set; }
        public ShipinkAddressDto address { get; set; }
    }
}
