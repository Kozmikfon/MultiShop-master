namespace MultiShop.Basket.Dtos
{
    public class BasketCheckoutDto
    {
        public string UserId { get; set; }

        // Adres ve İletişim Bilgileri
        public string AddressId { get; set; } // Order servisinde kayıtlı olan adresin ID'si
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressDetail { get; set; }
        
    }
}
