using MultiShop.Payment.Services.Abstract;

namespace MultiShop.Payment.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UpdateOrderPaymentStatusAsync(string basketId, bool isPaid)
        {
            var requestUri = "api/Orderings/UpdatePaymentStatus";

            var command = new
            {
                BasketId = basketId,
                IsPaid = isPaid
            };

            // PUT isteği gönderiyoruz
            var response = await _httpClient.PutAsJsonAsync(requestUri, command);

            // Eğer 200-299 arası bir kod dönerse başarılı sayıyoruz
            return response.IsSuccessStatusCode;
        }
    }
}
