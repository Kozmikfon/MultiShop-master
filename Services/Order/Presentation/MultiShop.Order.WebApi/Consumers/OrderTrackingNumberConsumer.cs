using MassTransit;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Shared.Enums;
using MultiShop.Shared.Events.Abstract;

namespace MultiShop.Order.WebApi.Consumers
{
    public class OrderTrackingNumberConsumer : IConsumer<IOrderTrackingNumberCreatedEvent>
    {
        private readonly IRepository<Ordering> _orderingRepository;

        public OrderTrackingNumberConsumer(IRepository<Ordering> orderingRepository)
        {
            _orderingRepository = orderingRepository;
        }

        public async Task Consume(ConsumeContext<IOrderTrackingNumberCreatedEvent> context)
        {
            var message = context.Message;

            // Veritabanından ilgili siparişi bul
            var order = await _orderingRepository.GetByIdAsync(message.OrderingId);

            if (order != null)
            {
                // Takip numarasını güncelle
                order.TrackingNumber = message.TrackingNumber;
                order.OrderStatus = OrderStatus.Shipped; 

                await _orderingRepository.UpdateAsync(order);

                Console.WriteLine($">>>>> [ORDER CONSUMER]: Sipariş {order.OrderingId} Takip No Güncellendi: {order.TrackingNumber} <<<<<");
            }
        }
    }
}
