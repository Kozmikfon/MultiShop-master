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

            // 🎯 Gelişmiş Kontrol: Hem boş mu bakıyoruz, hem de içinde "Hata" geçiyor mu?
            if (string.IsNullOrWhiteSpace(message.TrackingNumber) || message.TrackingNumber.Contains("Shipink Hatası"))
            {
                Console.WriteLine($">>>>> [İPTAL]: Sipariş {message.OrderingId} için GEÇERSİZ takip numarası (Hata Metni) geldi. DB güncellenmedi! <<<<<");
                return;
            }

            var order = await _orderingRepository.GetByIdAsync(message.OrderingId);

            if (order != null)
            {
                // ... geri kalan Idempotency ve Update işlemleri aynı
                if (order.TrackingNumber == message.TrackingNumber) return;

                order.TrackingNumber = message.TrackingNumber;
                order.OrderStatus = OrderStatus.Shipped;

                await _orderingRepository.UpdateAsync(order);
                Console.WriteLine($">>>>> [BAŞARILI]: Sipariş {order.OrderingId} Takip No Güncellendi: {order.TrackingNumber} <<<<<");
            }
        }
    }
}
