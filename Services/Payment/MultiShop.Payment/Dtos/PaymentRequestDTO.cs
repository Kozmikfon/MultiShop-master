namespace MultiShop.Payment.Dtos
{
    public class PaymentRequestDTO
    {
        // Kullanıcı Bilgileri
        public string BuyerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string RegistrationAddress { get; set; }
        public string City { get; set; }

        // Sepet/Ödeme Bilgileri
        public decimal Price { get; set; } // Ürünlerin toplamı
        public decimal PaidPrice { get; set; } // Kargo dahil son ödenecek tutar
        public string BasketId { get; set; } // Senin sistemindeki Sipariş No (Örn: MAHMUT-101)

        // iyzico'nun döneceği formun içine gömüleceği URL
        public string CallbackUrl { get; set; }
    }
}
