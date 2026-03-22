using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkOrderRequestDto
    {
        public ShipinkCustomerDto customer { get; set; }
        public List<ShipinkItemDto> items { get; set; }
        public string currency { get; set; } = "TRY";
        public decimal price { get; set; }
    }

}
