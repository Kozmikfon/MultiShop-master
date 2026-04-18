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
            var requestUri = "api/Orderings/UpdatePaymentStatus";
            var command = new { BasketId = basketId, IsPaid = isPaid };

            // 1. Önce Ordering API'yi güncelle (Veritabanı kaydı)
            var response = await _httpClient.PutAsJsonAsync(requestUri, command);

            if (response.IsSuccessStatusCode && isPaid)
            {
                // 2. 🚀 KRİTİK ADIM: Ödeme başarılıysa Cargo'yu tetikleyen event'i fırlat!
                // Not: Shared katmanındaki Interface'i kullanıyoruz.
                await _publishEndpoint.Publish<IOrderPaidEvent>(new
                {
                    OrderingId = int.Parse(basketId), // Eğer basketId içinde OrderingId taşıyorsan
                    
                });

                return true;
            }

            return false;
        }
    }
}
