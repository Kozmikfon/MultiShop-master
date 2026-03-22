using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkAddressDto
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; } = "34000"; // Dökümanda zorunlu, yoksa default veriyoruz
        public string country_code { get; set; } = "TR";
    }
}
