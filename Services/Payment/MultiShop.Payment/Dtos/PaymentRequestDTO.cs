using Iyzipay.Model;
using MultiShop.Basket.Dtos; // Senin BasketItemDto'nun olduğu yer

namespace MultiShop.Payment.Dtos
{
    public class PaymentRequestDTO
    {
        // --- Müşteri (Buyer) Bilgileri ---
        public string BuyerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }

        // --- Adres Bilgileri ---
        public string RegistrationAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; } // İlçe (iyzico sever)

        // --- Ödeme Tutarları ---
        public decimal Price { get; set; }      // Toplam Sepet Tutarı
        public decimal PaidPrice { get; set; }  // Ödenecek Tutar (İndirim varsa Price'tan farklı olabilir)

        // --- Sistem Bilgileri ---
        public string BasketId { get; set; }    // Hata veren meşhur alan
        public string CallbackUrl { get; set; } // Ödeme bitince dönülecek yer

        // --- Dinamik Sepet Ürünleri ---
        // Senin daha önce oluşturduğun BasketItemDto listesini buraya bağlıyoruz
       public List<BasketItemDto> BasketItems { get; set; }
    }
}