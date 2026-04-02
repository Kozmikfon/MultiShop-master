using MassTransit;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using MultiShop.Shared.Enums;
using MultiShop.Shared.Events;

namespace MultiShop.Cargo.WebApi.Consumers
{
    public class OrderPaidConsumer : IConsumer<IOrderPaidEvent>
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly ICargoDetailDal _cargoDetailDal; // Sorgu için gerekebilir
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderPaidConsumer(ICargoDetailService cargoDetailService, ICargoDetailDal cargoDetailDal, IPublishEndpoint publishEndpoint)
        {
            _cargoDetailService = cargoDetailService;
            _cargoDetailDal = cargoDetailDal;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IOrderPaidEvent> context)
        {
            // 1. Önce kargo kaydını bul
            var existingCargo = await _cargoDetailDal.GetByFilterAsync(x => x.OrderingId == context.Message.OrderingId);

            if (existingCargo != null)
            {
                // 🎯 Manager'daki profesyonel süreci başlat (API çağrısı + DB Update içeride yapılır)
                var trackingNumber = await _cargoDetailService.TCreateShipmentProcessAsync(existingCargo.CargoDetailId);

                // 2. Sipariş servisine geri bildirimi gönder
                await _publishEndpoint.Publish<IOrderCompletedEvent>(new
                {
                    OrderingId = context.Message.OrderingId,
                    TrackingNumber = trackingNumber,
                    Status = (int)OrderStatus.Shipped
                });
            }
            else
            {
                Console.WriteLine($">>>>> [HATA]: OrderingId {context.Message.OrderingId} için kargo kaydı bulunamadı! <<<<<");
            }
        }
    }
}