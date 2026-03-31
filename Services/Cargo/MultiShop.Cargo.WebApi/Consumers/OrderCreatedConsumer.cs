using MassTransit;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using MultiShop.Shared.Events;

namespace MultiShop.Cargo.WebApi.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreatedEvent>
    {
        private readonly ICargoDetailDal _cargoDetailDal;

        public OrderCreatedConsumer(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
        {
            var message = context.Message;

            var cargoDetail = new CargoDetail
            {
                OrderingId = message.OrderingId,
                ReceiverName = message.ReceiverName,
                ReceiverSurname = message.ReceiverSurname,
                ReceiverEmail = message.ReceiverEmail,
                ReceiverPhone = message.ReceiverPhone,
                ReceiverCity = message.ReceiverCity,
                ReceiverDistrict = message.ReceiverDistrict,
                ReceiverAddressDetail = message.ReceiverAddressDetail,
                ShipinkOrderId = message.ShipinkOrderId,

                // Zorunlu olabilecek alanlara varsayılan değerler veriyoruz
                SenderCustomer = message.SenderCustomer, // Veya message içinden gelmeli
                CargoCompanyId = message.CargoCompanyId, // Veritabanındaki geçerli bir ID olmalı
                CargoCustomerId = message.CargoCustomerId, // Veritabanındaki geçerli bir ID olmalı
                VendorId = message.VendorId,

                CurrentStatus = CargoStatus.Created,
                Barcode = $"BRK-{message.OrderingId}-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}",

                // Boyutlar için varsayılan değer
                Weight = message.Weight,
                Width = message.Width ,
                Height = message.Height ,
                Length = message.Length ,
            };

            await _cargoDetailDal.Insert(cargoDetail);
            Console.WriteLine($">>>>> CARGO: Sipariş {message.OrderingId} için ilk kayıt (Pending) açıldı. <<<<<");
        }
    }
}
