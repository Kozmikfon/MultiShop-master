using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkShipmentRequestDto
    {
        [JsonPropertyName("direction")]
        public string direction { get; set; } = "outgoing";

        [JsonPropertyName("order_id")]
        public string order_id { get; set; }

        [JsonPropertyName("carrier_service_id")]
        public string carrier_service_id { get; set; }

        [JsonPropertyName("carrier_account_id")]
        public string carrier_account_id { get; set; }

        [JsonPropertyName("warehouse_id")]
        public string warehouse_id { get; set; }

        [JsonPropertyName("card_id")]
        public string card_id { get; set; }

        [JsonPropertyName("sales_invoice")]
        public SalesInvoice sales_invoice { get; set; }

        [JsonPropertyName("packages")]
        public List<ShipinkPackageDto> packages { get; set; }

        [JsonPropertyName("create_invoice")]
        public bool create_invoice { get; set; }
    }
}
