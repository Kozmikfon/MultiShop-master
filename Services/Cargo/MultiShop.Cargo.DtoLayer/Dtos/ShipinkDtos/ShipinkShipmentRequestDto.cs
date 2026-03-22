namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkShipmentRequestDto
    {
        public string warehouse_id { get; set; }
        public string carrier_account_id { get; set; }
        public string carrier_service_id { get; set; }
        public string direction { get; set; } = "outgoing";

        // Alıcı bilgilerini tutan yeni obje
        public ShipinkOrderDto order { get; set; }

        // Paket bilgilerini tutan liste
        public List<ShipinkPackageDto> packages { get; set; }
    }

    public class ShipinkOrderDto
    {
        public string customer_order_number { get; set; } // Senin sistemindeki Sipariş No
        public string receiver_name { get; set; }
        public string receiver_address { get; set; }
        public string receiver_city { get; set; }
        public string receiver_district { get; set; }
        public string receiver_phone { get; set; }
    }
}