using MassTransit;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using MultiShop.Shared.Events;

namespace MultiShop.Cargo.WebApi.Consumers
{
    public class OrderPaidConsumer : IConsumer<IOrderPaidEvent>
    {
        private readonly IShipinkService _shipinkService;
        private readonly ICargoDetailDal _cargoDetaildal;

        public OrderPaidConsumer(IShipinkService shipinkService, ICargoDetailDal cargoDetaildal)
        {
            _shipinkService = shipinkService;
            _cargoDetaildal = cargoDetaildal;
        }

        // DİKKAT: Buraya 'async' ekledik!
        public async Task Consume(ConsumeContext<IOrderPaidEvent> context)
        {
            var cargoDetail = new CargoDetail
            {
                OrderingId = context.Message.OrderingId,
                ReceiverName = context.Message.ReceiverName,
                ReceiverSurname = context.Message.ReceiverSurname,
                ReceiverEmail = context.Message.ReceiverEmail,
                ReceiverPhone = context.Message.ReceiverPhone,
                ReceiverCity = context.Message.ReceiverCity,
                ReceiverDistrict = context.Message.ReceiverDistrict,
                ReceiverAddressDetail = context.Message.ReceiverAddressDetail,
                CargoCompanyId = context.Message.CargoCompanyId,
                CurrentStatus = CargoStatus.Created,
                Weight = 1.0,
                Width = 10,
                Height = 10,
                Length = 10,
                // Değişken ismini düzelttik: _cargoDetaildal
                Barcode = "BRK-" + context.Message.OrderingId + "-" + DateTime.Now.Ticks.ToString().Substring(10)
            };

            // await kullandık
            await _cargoDetaildal.Insert(cargoDetail);

            try
            {
                // Shipink tetikleme
                await _shipinkService.CreateShipmentAsync(cargoDetail.CargoDetailId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Shipink Hatası: {ex.Message}");
                throw;
            }
        }
    }
}