namespace MultiShop.Basket.Dtos
{
    public class BasketCheckoutDto
    {
        public string UserId { get; set; }
        public string AddressId { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressDetail { get; set; }
        //public string? VendorId { get; set; } 
        
    }
}
