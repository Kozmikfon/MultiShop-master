using MediatR;
using MultiShop.Order.Application.Dtos.ExternalDtos;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommand>
    {
        private readonly IRepository<Ordering> _orderingRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IShipinkService _shipinkService; // 👈 Artık nesnemiz hazır!

        public CreateOrderingCommandHandler(
            IRepository<Ordering> repository,
            IHttpClientFactory httpClientFactory,
            IRepository<Address> address,
            IShipinkService shipinkService)
        {
            _orderingRepository = repository;
            _httpClientFactory = httpClientFactory;
            _addressRepository = address;
            _shipinkService = shipinkService;
        }

        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Adresi Getir (Shipink için zorunlu)
            var address = await _addressRepository.GetByIdAsync(request.AddressId);
            if (address == null) return;

            string shipinkId = string.Empty;

            try
            {
                // 2. ADIM: 🔥 SHIPINK'TE SİPARİŞİ OLUŞTUR 🔥
                // Bu metot senin yazdığın Manager içindeki /orders endpoint'ine gider.
                shipinkId = await _shipinkService.CreateOrderAsync(request, address);
            }
            catch (Exception ex)
            {
                // Shipink hata verirse siparişi SQL'e yazmıyoruz!
                throw new Exception($"Shipink Kayıt Hatası: {ex.Message}");
            }

            // 3. ADIM: Kendi Veritabanına Kaydet (Shipink ID'si ile beraber)
            var ordering = new Ordering
            {
                OrderDate = DateTime.Now,
                TotalPrice = request.TotalPrice,
                UserId = request.UserId,
                AddressId = request.AddressId,
                ShipinkOrderId = shipinkId // 👈 SQL'e bu ID ile mühürlendi
            };
            await _orderingRepository.CreateAsync(ordering);

            // 4. ADIM: Cargo Mikroservisine Haber Ver (Kuyruk veya HTTP)
            // 4. ADIM: Cargo Mikroservisine Haber Ver
            try
            {
                var client = _httpClientFactory.CreateClient();
                // 4. ADIM: Cargo Mikroservisine Haber Ver
                var cargoDto = new CreateCargoDetailDto
                {
                    // Adres ve Müşteri Bilgileri (Shipink'in /orders yapısına uygun)
                    ReceiverName = address.Name,
                    ReceiverSurname = address.Surname,
                    ReceiverPhone = address.Phone, // Örn: 90555...
                    ReceiverEmail = address.Email,
                    ReceiverAddress = address.Detail1,
                    ReceiverAddressDetail = address.Detail2, // Boş kalırsa Cargo hata verir, doldurduk!
                    ReceiverCity = address.District, // Shipink 'city' (Merkez)
                    ReceiverDistrict = address.City, // Shipink 'state' (Elazığ)
                    CargoCompanyId=request.CargoCompanyId, // Şimdilik direkt gönderelim, Cargo kendi içinde eşleştirebilir
                    CargoCustomerId=request.CargoCustomerId, // Şimdilik direkt gönderelim, Cargo kendi içinde eşleştirebilir

                    // Shipink Sevkiyat (Shipment) UUID'leri (Postman'den aldıkların)
                    ShipinkOrderId = shipinkId,
                    CarrierAccountId = "cb333299-1001-405a-ad7a-613b2aff1b80", // PTT Hesabı
                    WarehouseId = "7ad6e1be-3d3f-4769-aa51-8531cb096186", // Depo ID
                    CarrierServiceId = "ptt_standart",

                    // Diğer Zorunlu Alanlar
                    Barcode = Guid.NewGuid().ToString().Substring(0, 10),
                    VendorId = "3",
                    OrderingId = ordering.OrderingId,
                    SenderCustomer = request.SenderCustomer ?? "MultiShop Online",
                    
                    Weight=1.5,
                    Height=10,
                    Length=10,
                    Width=10,


                    
                };

                // İstek gönderimi
                var response = await client.PostAsJsonAsync("https://localhost:7073/api/CargoDetails", cargoDto);

                if (!response.IsSuccessStatusCode)
                {
                    // Cargo servisi hata dönerse nedenini konsola yazdıralım
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($">>>>> CARGO HATASI: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Bağlantı koparsa veya SSL hatası olursa buraya düşer
                Console.WriteLine($">>>>> CARGO BAĞLANTI HATASI: {ex.Message}");
            }
        }
    }
}