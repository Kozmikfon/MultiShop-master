using MassTransit;
using MediatR;
using MultiShop.Order.Application.Dtos.ExternalDtos;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Shared.Events;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommand>
    {
        private readonly IRepository<Ordering> _orderingRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IPublishEndpoint _publishEndpoint; // RabbitMQ için
        private readonly IShipinkService _shipinkService; // 👈 Artık nesnemiz hazır!

        public CreateOrderingCommandHandler(
            IRepository<Ordering> repository,

            IRepository<Address> address,
            IShipinkService shipinkService,
            IPublishEndpoint publishEndpoint)
        {
            _orderingRepository = repository;

            _addressRepository = address;
            _shipinkService = shipinkService;
            _publishEndpoint = publishEndpoint;
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
                await _publishEndpoint.Publish<IOrderCreatedEvent>(new
                {
                    OrderingId = ordering.OrderingId,
                    UserId = request.UserId,
                    TotalPrice = request.TotalPrice,

                    // Kargo servisi için gerekli adres bilgileri
                    ReceiverName = address.Name,
                    ReceiverSurname = address.Surname,
                    ReceiverEmail = address.Email,
                    ReceiverPhone = address.Phone,
                    ReceiverCity = address.District, // Shipink City
                    ReceiverDistrict = address.City,  // Shipink State
                    ReceiverAddressDetail = $"{address.Detail1} {address.Detail2}",

                    // Shipink UUID'leri
                    ShipinkOrderId = shipinkId,

                    // Varsayılan Kargo Bilgileri
                    CargoCompanyId = request.CargoCompanyId != 0 ? request.CargoCompanyId : 3005,
                    Weight = 1.0,
                    Width = 10,
                    Height = 10,
                    Length = 10
                }, cancellationToken);

                Console.WriteLine($">>>>> ORDER {ordering.OrderingId} OLUŞTURULDU VE RABBITMQ'YA FIRLATILDI! <<<<<");
            }
            catch (Exception ex)
            {
                // Bağlantı koparsa veya SSL hatası olursa buraya düşer
                Console.WriteLine($">>>>> CARGO BAĞLANTI HATASI: {ex.Message}");
            }
        }
    }
}