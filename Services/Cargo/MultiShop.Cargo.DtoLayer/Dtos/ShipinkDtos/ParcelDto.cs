using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ParcelDto
    {
        // Paketin ağırlığı (Desi hesaplama için)
        public double Weight { get; set; }
        public int Quantity { get; set; }
    }
}
