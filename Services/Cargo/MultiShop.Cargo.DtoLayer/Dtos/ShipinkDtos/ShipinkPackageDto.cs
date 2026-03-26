using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkPackageDto
    {
        public string dimension_unit { get; set; } = "cm";
        public int height { get; set; }
        public int length { get; set; }
        public int width { get; set; }
        public double weight { get; set; }
        public string weight_unit { get; set; } = "kg";
    }

}
