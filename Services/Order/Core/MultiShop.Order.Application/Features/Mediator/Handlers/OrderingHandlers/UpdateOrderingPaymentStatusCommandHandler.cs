using MassTransit;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Domain.Entities.Enums; // Enum için
using MultiShop.Shared.Events; // Ortak kütüphanen

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
            if (int.TryParse(request.BasketId, out int orderingId))
            {
                var values = await _repository.GetByIdAsync(orderingId);

                if (values != null)
                {
                    // 1. Durumu senin yeni Enum'ınla güncelle
                    values.PaymentStatus = request.IsPaid ? PaymentStatus.Completed : PaymentStatus.Failed;
                    await _repository.UpdateAsync(values);

                    // 2. Eğer ödeme başarılıysa Kargo mesajını fırlat
                    if (request.IsPaid)
                    {
                        // Adres detaylarını getir (Kargo için şart)
                        var address = await _addressRepository.GetByIdAsync(values.AddressId);

                        if (address != null)
                        {
                            await _publishEndpoint.Publish<IOrderPaidEvent>(new
                            {
                                OrderingId = values.OrderingId,
                                ReceiverName = address.Name,
                                ReceiverSurname = address.Surname,
                                ReceiverEmail = address.Email,
                                ReceiverPhone = address.Phone,
                                ReceiverCity = address.City,
                                ReceiverDistrict = address.District,
                                ReceiverAddressDetail = $"{address.Detail1} {address.Detail2} {address.Description}",
                                CargoCompanyId = 1 // Şimdilik varsayılan PTT
                            }, cancellationToken);
                        }
                    }
                }
            }
        }
    }
}