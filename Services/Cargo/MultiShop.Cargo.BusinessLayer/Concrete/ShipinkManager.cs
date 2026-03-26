using Microsoft.Extensions.Options;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Settings;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class ShipinkManager : IShipinkService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICargoDetailDal _cargoDetailDal;
        private readonly ShipinkSettings _settings;

        public ShipinkManager(IHttpClientFactory httpClientFactory,
                              ICargoDetailDal cargoDetailDal,
                              IOptions<ShipinkSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _cargoDetailDal = cargoDetailDal;
            _settings = settings.Value;
        }

        public async Task<string> CreateShipmentAsync(int cargoDetailId)
        {
            // 1. DİNAMİK VERİ: Include ile firmayı çekiyoruz (GetCargoDetailWithCompany DAL'da yazılmalı)
            var cargo = await _cargoDetailDal.GetCargoDetailWithCompany(cargoDetailId);
            if (cargo == null) return "Kargo kaydı bulunamadı.";

            var client = _httpClientFactory.CreateClient();
            var token = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(token)) return "Yetkilendirme hatası.";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 2. TEMİZ VE DİNAMİK PAYLOAD: Postman'da çalışan en sade yapı
            var shipmentRequest = new ShipinkShipmentRequestDto
            {
                order_id = $"{cargo.OrderingId}-{cargo.CargoDetailId}",
                carrier_account_id = cargo.CargoCompany.CarrierAccountId, // DB'den gelen dinamik ID
                carrier_service_id = cargo.CargoCompany.CarrierServiceId, // DB'den gelen dinamik Servis
                warehouse_id = _settings.WarehouseId,
                card_id = _settings.CardId, // Postman testimizdeki kritik alan
                packages = new List<ShipinkPackageDto>
        {
            new ShipinkPackageDto
            {
                weight = cargo.Weight > 0 ? cargo.Weight : 1.0,
                width = cargo.Width > 0 ? cargo.Width : 10,
                height = cargo.Height > 0 ? cargo.Height : 10,
                length = cargo.Length > 0 ? cargo.Length : 10
            }
        }
            };

            // 3. İSTEK GÖNDER (Artık DTO ile çok daha temiz)
            var response = await client.PostAsJsonAsync($"{_settings.BaseUrl}/shipments", shipmentRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Shipink Hatası: {error}";
            }

            // 4. BAŞARILI: Tip güvenli şekilde veriyi okuyoruz
            var result = await response.Content.ReadFromJsonAsync<ShipinkShipmentResponseDto>();

            if (result != null && result.success)
            {
                cargo.TrackingNumber = result.data.carrier.shipment_id;
                cargo.ShipinkShipmentId = result.data.id;
                cargo.CurrentStatus = CargoStatus.LabelCreated; // Enum kullanımı

                await _cargoDetailDal.Update(cargo);
                return $"Başarılı! Takip No: {cargo.TrackingNumber}";
            }

            return "İşlem başarılı dendi ama veri okunamadı.";
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var loginData = new { username = "baglamamahmut9@gmail.com", password = "Kozshipink0." };

            var response = await client.PostAsJsonAsync($"{_settings.BaseUrl}/token", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return result.GetProperty("access_token").GetString();
            }
            return null;
        }

        public async Task<bool> UpdateStatusAsync(int orderingId, string newStatus)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PatchAsJsonAsync($"{_settings.BaseUrl}/shipments/{orderingId}/status", new { status = newStatus });
            return response.IsSuccessStatusCode;
        }
    }
}