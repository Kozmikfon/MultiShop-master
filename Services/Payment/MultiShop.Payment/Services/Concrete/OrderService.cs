using MassTransit;
using MultiShop.Payment.Services.Abstract;
using MultiShop.Shared.Events.Abstract;

namespace MultiShop.Payment.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPublishEndpoint _publishEndpoint; // MassTransit'in PublishEndpoint'i
        public OrderService(HttpClient httpClient, IPublishEndpoint publishEndpoint)
        {
            _httpClient = httpClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> UpdateOrderPaymentStatusAsync(string basketId, bool isPaid)
        {
            // 1. Önce Sipariş Durumunu HTTP ile Güncelle (Senin yaptığın kısım)
            var response = await _httpClient.PutAsJsonAsync("api/Orderings/UpdatePaymentStatus", new { BasketId = basketId, IsPaid = isPaid });

            if (response.IsSuccessStatusCode && isPaid)
            {
                // 2. 🚀 BURASI KRİTİK: Ödeme başarılıysa Cargo Servisini tetikleyecek Event'i fırlat!
                // Not: PublishEndpoint'i constructor'da inject etmelisin (MassTransit)
                await _publishEndpoint.Publish<IOrderPaidEvent>(new
                {
                    OrderingId = int.Parse(basketId), // BasketId'yi OrderingId olarak kullandığını varsayıyorum
                                                      // ... Diğer alıcı bilgilerini de event'e ekle
                });
            }
            return response.IsSuccessStatusCode;
        }
    }
}
