namespace MultiShop.Payment.Dtos
{
    public class BasketItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // Bu eksikti, mutlaka ekle!
        public string Category1 { get; set; }
        public string ProductImageUrl { get; set; } // test.html'den gönderdiğimiz için burada da olsun
        public string VendorId { get; set; }
    }
}
