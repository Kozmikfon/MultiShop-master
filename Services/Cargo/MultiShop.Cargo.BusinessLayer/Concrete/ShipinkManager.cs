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
using System.Text.Json.Serialization;

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
            var cargo = await _cargoDetailDal.GetCargoDetailWithCompany(cargoDetailId);
            if (cargo == null) return "Kargo kaydı bulunamadı.";

            var client = _httpClientFactory.CreateClient();
            var token = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(token)) return "Yetkilendirme hatası.";

            // Header'ları temizleyelim ve baştan kuralım
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // KRİTİK
            client.DefaultRequestHeaders.Add("X-Language", "TR");

            var shipmentRequest = new ShipinkShipmentRequestDto
            {
                direction = "outgoing",
                order_id = cargo.ShipinkOrderId, // GUID + :ID formatı
                carrier_account_id = cargo.CargoCompany.CarrierAccountId,
                carrier_service_id = cargo.CargoCompany.CarrierServiceId,
                warehouse_id = _settings.WarehouseId,
                card_id = _settings.CardId,
                sales_invoice = new SalesInvoice
                {
                    no = $"EFA{DateTime.Now:yyyyMMddHHmm}{cargoDetailId}",
                    date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    url = "https://example.com/invoice.pdf"
                },
                packages = new List<ShipinkPackageDto>
        {
            new ShipinkPackageDto
            {
                dimension_unit = "cm",
                weight_unit = "kg",
                weight = cargo.Weight > 0 ? (double)cargo.Weight : 1.0,
                width = cargo.Width > 0 ? (int)cargo.Width : 15,
                height = cargo.Height > 0 ? (int)cargo.Height : 10,
                length = cargo.Length > 0 ? (int)cargo.Length : 20
            }
        },
                create_invoice = false,
            };

            // KRİTİK: DTO etiketlerini ezmemesi için PropertyNamingPolicy null olmalı
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            // DEBUG: Buradaki çıktıyı mutlaka Postman'e yapıştırıp dene
            var jsonDebug = JsonSerializer.Serialize(shipmentRequest, jsonOptions);
            Console.WriteLine($">>>>> GİDEN JSON: {jsonDebug}");

            // DİKKAT: jsonOptions parametresini eklemeyi unutma!
            var response = await client.PostAsJsonAsync($"{_settings.BaseUrl}/shipments", shipmentRequest, jsonOptions);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                // Hatanın detayını daha net görmek için:
                return $"Shipink Hatası ({(int)response.StatusCode}): {error}";
            }

            var result = await response.Content.ReadFromJsonAsync<ShipinkShipmentResponseDto>(jsonOptions);

            if (result != null && result.success)
            {
                cargo.TrackingNumber = result.data.carrier.shipment_id;
                cargo.ShipinkShipmentId = result.data.id;
                cargo.CurrentStatus = CargoStatus.LabelCreated;

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

        public async Task<bool> UpdateStatusAsync(string shipinkId, string newStatus)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PatchAsJsonAsync($"{_settings.BaseUrl}/shipments/{shipinkId}/status", new { status = newStatus });
            return response.IsSuccessStatusCode;
        }
    }
}