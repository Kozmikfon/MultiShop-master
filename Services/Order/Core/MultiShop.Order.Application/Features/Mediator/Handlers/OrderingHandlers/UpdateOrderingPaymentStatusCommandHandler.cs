using MassTransit;
using MassTransit.Transports;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Domain.Entities.Enums; // Enum için
using MultiShop.Shared.Events.Abstract; // Ortak kütüphanen

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class UpdateOrderingPaymentStatusCommandHandler : IRequestHandler<UpdateOrderingPaymentStatusCommand>
    {
        private readonly IRepository<Ordering> _repository;
        private readonly IRepository<Address> _addressRepository; // Adres için
        private readonly IPublishEndpoint _publishEndpoint; // RabbitMQ için

        public UpdateOrderingPaymentStatusCommandHandler(
            IRepository<Ordering> repository,
            IRepository<Address> addressRepository,
            IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _addressRepository = addressRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(UpdateOrderingPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            // 1. ID Dönüşümü (Eğer veritabanında int ise parse etmeliyiz)
            if (!int.TryParse(request.OrderingId, out int orderId)) return;

            var values = await _repository.GetByIdAsync(orderId);
            if (values == null) return;

            // 2. Ödeme durumunu güncelle
            values.PaymentStatus = PaymentStatus.Completed;
            await _repository.UpdateAsync(values);

            // 3. Adres bilgilerini getir
            var address = await _addressRepository.GetByIdAsync(values.AddressId);

            // 4. 🔥 ASIL OLAY: Cargo Servisini Tetikle 🔥
            // Shared kütüphanendeki IOrderPaidEvent property isimleriyle birebir aynı olmalı!
            await _publishEndpoint.Publish<IOrderPaidEvent>(new
            {
                OrderingId = values.OrderingId,
                // EĞER IOrderPaidEvent içinde 'ShipinkOrderId' yoksa burayı silebilirsin 
                // veya interface'e ekleyebilirsin.

                ReceiverName = address.Name,
                ReceiverSurname = address.Surname,
                ReceiverEmail = address.Email,
                ReceiverPhone = address.Phone,
                ReceiverCity = address.City,
                ReceiverDistrict = address.District,
                ReceiverAddressDetail = address.Detail1,
                SenderCustomer="multi",
                CargoCompanyId = 3005,
                CargoCustomerId = 1, // Interface'de varsa eklemeyi unutma
                Weight = values.TotalWeight,
                Width = 10,
                Height = 10,
                Length = 10
            });

            Console.WriteLine($">>>>> SİPARİŞ {values.OrderingId} ÖDENDİ. EVENT FIRLATILDI! <<<<<");
        }
    }
}