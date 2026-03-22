using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkPackageDto
    {
        public double weight { get; set; } = 1.0;
        public string weight_unit { get; set; } = "kg";
        public int length { get; set; } = 10;
        public int width { get; set; } = 10;
        public int height { get; set; } = 10;
        public string dimension_unit { get; set; } = "cm";
    }
}
