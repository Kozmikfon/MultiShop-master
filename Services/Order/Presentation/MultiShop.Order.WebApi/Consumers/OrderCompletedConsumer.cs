using MassTransit;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Shared.Enums;
using MultiShop.Shared.Events;

    namespace MultiShop.Order.WebApi.Consumers
    {
        public class OrderCompletedConsumer : IConsumer<IOrderCompletedEvent>
        {
            private readonly IRepository<Ordering> _orderingRepository;

            public OrderCompletedConsumer(IRepository<Ordering> orderingRepository)
            {
                _orderingRepository = orderingRepository;
            }

            public async Task Consume(ConsumeContext<IOrderCompletedEvent> context)
            {
                // 1. Gelen mesajdaki OrderingId ile siparişi bul
                var order = await _orderingRepository.GetByIdAsync(context.Message.OrderingId);

                if (order != null)
                {
                    // 2. Takip numarasını işle ve durumu güncelles
                    order.TrackingNumber = context.Message.TrackingNumber;
                    // Eğer bir 'OrderStatus' enum'ın varsa 'Shipped' yapabilirsin

                    order.OrderStatus = (OrderStatus)context.Message.Status;
                    await _orderingRepository.UpdateAsync(order);

                    Console.WriteLine($">>>>> ORDER {order.OrderingId}: Kargo Takip No ({order.TrackingNumber}) başarıyla işlendi. <<<<<");
                }
            }
        }
    }
