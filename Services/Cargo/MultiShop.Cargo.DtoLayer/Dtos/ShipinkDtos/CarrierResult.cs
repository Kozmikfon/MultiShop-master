using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class CarrierResult
    {
        public string shipment_id { get; set; } // İşte bu bizim Takip Numaramız
        public string tracking_url { get; set; }
    }
}
