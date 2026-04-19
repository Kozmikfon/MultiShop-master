using Iyzipay.Model;
using MultiShop.Basket.Dtos; // Senin BasketItemDto'nun olduğu yer

namespace MultiShop.Payment.Dtos
{
    public class PaymentRequestDTO
    {
        // Temel Bilgiler
        public decimal Price { get; set; }
        public decimal PaidPrice { get; set; }
        public string BasketId { get; set; }
        public string CallbackUrl { get; set; }

        // Buyer (Alıcı) Bilgileri
        public string BuyerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GsmNumber { get; set; }
        public string Email { get; set; }
        public string RegistrationAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        public string ZipCode { get; set; } // Eklendi
        public string IdentityNumber { get; set; } // Eklendi
        public string IpAddress { get; set; } // Eklendi

        // Sepet İçeriği
        public List<BasketItemDto> BasketItems { get; set; }
    }
}