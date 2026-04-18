using AutoMapper;
using MassTransit;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using MultiShop.Shared.Events.Concrete;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public BasketService(RedisService redisService, IHttpClientFactory httpClientFactory, IConfiguration configuration, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _redisService = redisService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Checkout(BasketCheckoutDto basketCheckoutDto)
        {
            var basket = await GetBasket(basketCheckoutDto.UserId);
            if (basket == null || basket.BasketItems.Count == 0) return false;  
            
            var checkoutEvent = _mapper.Map<BasketCheckoutEvent>(basket);

            
            _mapper.Map(basketCheckoutDto, checkoutEvent);

            
            Console.WriteLine($">>>>> [CHECKOUT]: {checkoutEvent.Name} {checkoutEvent.Surname} | Ağırlık: {checkoutEvent.TotalWeight} kg | Fiyat: {checkoutEvent.TotalPrice} TL <<<<<");

            await _publishEndpoint.Publish(checkoutEvent);
            await DeleteBasket(basketCheckoutDto.UserId);
            return true;
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

            // 2. ADIM: JSON okurken harf duyarlılığını kapatıyoruz
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket, options);
        }
        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            var client = _httpClientFactory.CreateClient();
            var catalogApiUrl = _configuration["CatalogApiUrl"];
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // 🚀 ADIM A: Önce mevcut sepeti Redis'ten çekiyoruz
            var currentBasket = await GetBasket(basketTotalDto.UserId);

            foreach (var incomingItem in basketTotalDto.BasketItems)
            {
                // 🚀 ADIM B: Gelen ürün zaten sepette var mı?
                var existingItem = currentBasket.BasketItems.FirstOrDefault(x => x.ProductId == incomingItem.ProductId);

                if (existingItem != null)
                {
                    // Varsa: Sadece miktarını artırıyoruz
                    existingItem.Quantity += incomingItem.Quantity;
                }
                else
                {
                    // Yoksa: Önce Katalog'dan fiziksel verilerini çekiyoruz
                    try
                    {
                        var response = await client.GetAsync($"{catalogApiUrl}/products/{incomingItem.ProductId}");
                        if (response.IsSuccessStatusCode)
                        {
                            var productDetail = await response.Content.ReadFromJsonAsync<CatalogProductDto>(options);
                            if (productDetail != null)
                            {
                                if (productDetail.Weight > 0) incomingItem.Weight = productDetail.Weight;
                                if (productDetail.Width > 0) incomingItem.Width = productDetail.Width;
                                if (productDetail.Height > 0) incomingItem.Height = productDetail.Height;
                                if (productDetail.Length > 0) incomingItem.Length = productDetail.Length;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($">>>>> Catalog API Hatası: {ex.Message} <<<<<");
                    }

                    // Yeni ürünü listeye ekle
                    currentBasket.BasketItems.Add(incomingItem);
                }
            }

            // 🚀 ADIM C: Birleşmiş ve güncellenmiş sepeti Redis'e yazıyoruz
            await _redisService.GetDb().StringSetAsync(currentBasket.UserId, JsonSerializer.Serialize(currentBasket));
        }

    }
    
}