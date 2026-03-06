using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class CreateShipinkShipmentDto
    {
        public string OrderingId { get; set; }

        // Alıcı Bilgileri
        public ReceiverDto Receiver { get; set; }

        // Paket Bilgileri
        public List<ParcelDto> Parcels { get; set; }

        // Seçilen Kargo Firması (Örn: "aras", "yurtici")
        public string CarrierKey { get; set; }
    }
}
