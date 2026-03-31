using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public BasketService(RedisService redisService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _redisService = redisService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task DeleteBasket(string userId)
        {
            await _redisService.GetDb().KeyDeleteAsync(userId);
        }
        public async Task<BasketTotalDto> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (string.IsNullOrEmpty(existBasket))
            {
                return new BasketTotalDto { UserId = userId, BasketItems = new List<BasketItemDto>() };
            }

            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
        }
        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            var client = _httpClientFactory.CreateClient();
            // Appsettings.json içinde "CatalogApiUrl" tanımlı olmalı. Örn: http://localhost:7070/api
            var catalogApiUrl = _configuration["CatalogApiUrl"];

            // 🚀 KRİTİK NOKTA: Sepetteki her ürünü kargo verileriyle dolduruyoruz
            foreach (var item in basketTotalDto.BasketItems)
            {
                try
                {
                    // Catalog API'den ürünün fiziksel özelliklerini çekiyoruz
                    var response = await client.GetAsync($"{catalogApiUrl}/products/{item.ProductId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var productDetail = await response.Content.ReadFromJsonAsync<CatalogProductDto>();

                        if (productDetail != null)
                        {
                            item.Weight = productDetail.Weight;
                            item.Width = productDetail.Width;
                            item.Height = productDetail.Height;
                            item.Length = productDetail.Length;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Catalog kapalıysa veya hata verirse sipariş aksamasın diye varsayılan değer atıyoruz
                    Console.WriteLine($"Catalog API Hatası: {ex.Message}");
                    item.Weight = 1.0;
                }
            }

            // Artık kargo bilgileri dolu olan sepeti Redis'e yazıyoruz
            await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId, JsonSerializer.Serialize(basketTotalDto));
        }

    }
    
}