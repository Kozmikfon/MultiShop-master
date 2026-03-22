using Microsoft.Extensions.Options;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Settings;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
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
            // 1. Veritabanından kargo detayını çek
            var cargo = await _cargoDetailDal.GetById(cargoDetailId);
            if (cargo == null) return "Veritabanında kargo kaydı bulunamadı.";

            var client = _httpClientFactory.CreateClient();
            var token = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(token)) return "Shipink yetkilendirme hatası (Token alınamadı).";
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 2. EMBEDDED PAYLOAD: Sipariş ve Kargo Bilgileri Tek Pakette
            // Bu yöntem "order_id bulunamadı" hatasını kökten çözer.
            var shipmentRequest = new
            {
                order = new
                {
                    external_id = cargo.OrderingId.ToString(), 
                    customer = new
                    {
                        name = $"{cargo.ReceiverName} {cargo.ReceiverSurname}",
                        email = new { main = cargo.ReceiverEmail },
                        phone = new { main = cargo.ReceiverPhone },
                        address = new
                        {
                            street = cargo.ReceiverAddressDetail,
                            city = cargo.ReceiverCity,
                            state = cargo.ReceiverCity,
                            zip = "34000", 
                            country_code = "TR"
                        }
                    },
                    items = new List<object> {
                        new {
                            name = "Sipariş No: " + cargo.OrderingId,
                            quantity = 1,
                            price = 1 
                        }
                    },
                    price = 1,
                    currency = "TRY"
                },
                carrier_service_id = _settings.CarrierServiceId,
                carrier_account_id = _settings.CarrierAccountId,
                warehouse_id = _settings.WarehouseId,
                direction = "outgoing",
                packages = new List<object>
                {
                    new { weight = 1, weight_unit = "kg" }
                }
            };

            // 3. İstek Gönder
            var response = await client.PostAsJsonAsync($"{_settings.BaseUrl}/shipments", shipmentRequest);
            var resultText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"HATA DETAYI: {resultText}";
            }

            // 4. BAŞARILI: Takip numarasını yakala ve DB'ye yaz
            try 
            {
                var resultJson = JsonSerializer.Deserialize<JsonElement>(resultText);
                var data = resultJson.GetProperty("data");

                // Shipink'in kendi ID'lerini ve takip numarasını alıyoruz
                string shipinkOrderId = data.GetProperty("order_id").GetString();
                string shipinkShipmentId = data.GetProperty("id").GetString();
                string trackingNumber = data.GetProperty("carrier").GetProperty("shipment_id").GetString();

                // Entity güncelleme
                cargo.ShipinkOrderId = shipinkOrderId;
                cargo.ShipinkShipmentId = shipinkShipmentId;
                cargo.TrackingNumber = trackingNumber;
                //cargo.StatusDescription = "Kargo Hazırlandı / Takip No Alındı";

                await _cargoDetailDal.Update(cargo);

                return $"Başarılı! Takip No: {trackingNumber}";
            }
            catch (Exception ex)
            {
                return $"Kargo oluştu ama DB güncellenemedi: {ex.Message}. Yanıt: {resultText}";
            }
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