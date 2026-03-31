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
        private readonly IShipinkService _shipinkService;
        private readonly ICargoDetailDal _cargoDetaildal;
        private readonly IPublishEndpoint _publishEndpoint; // 👈 Geri bildirim için eklendi

        public OrderPaidConsumer(
            IShipinkService shipinkService,
            ICargoDetailDal cargoDetaildal,
            IPublishEndpoint publishEndpoint)
        {
            _shipinkService = shipinkService;
            _cargoDetaildal = cargoDetaildal;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IOrderPaidEvent> context)
        {
            Console.WriteLine($">>>>> ÖDEME ONAYI GELDİ: Sipariş ID {context.Message.OrderingId} <<<<<");

            try
            {
                // 1. ADIM: Zaten oluşturulmuş olan kargo kaydını bul (OrderCreated'da açmıştık)
                var existingCargo = await _cargoDetaildal.GetByFilterAsync(x => x.OrderingId == context.Message.OrderingId);

                if (existingCargo != null)
                {
                    // Kayıt varsa güncelle
                    existingCargo.CurrentStatus = CargoStatus.Created;
                    await _cargoDetaildal.Update(existingCargo);

                    // 2. ADIM: Shipink Sürecini Başlat
                    var result = await _shipinkService.CreateShipmentAsync(existingCargo.CargoDetailId);

                    existingCargo.CurrentStatus = CargoStatus.LabelCreated;
                    existingCargo.TrackingNumber=result.ToString();

                    await _cargoDetaildal.Update(existingCargo);

                    await _publishEndpoint.Publish<IOrderCompletedEvent>(new
                    {
                        OrderingId = context.Message.OrderingId,
                        TrackingNumber = result.ToString(), // Shipink'ten dönen takip nosu
                        Status = (int)OrderStatus.Shipped
                    });

                    Console.WriteLine($"✅ Adım 3: IOrderCompletedEvent fırlatıldı. Takip No: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hata: Kargo süreci tamamlanamadı! {ex.Message}");
                throw;
            }
        }
    }
}