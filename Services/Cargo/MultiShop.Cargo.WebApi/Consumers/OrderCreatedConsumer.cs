using MassTransit;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Shared.Events.Abstract;

namespace MultiShop.Cargo.WebApi.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreatedEvent>
    {
        // 🚀 DAL'ı değil, Service/Manager katmanını çağırıyoruz
        private readonly ICargoDetailService _cargoDetailService;

        public OrderCreatedConsumer(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }

        public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
        {
            var message = context.Message;

            // 🎯 Consumer sadece DTO hazırlar (Veriyi paketler)
            var createDto = new CreateCargoDetailDto
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
                SenderCustomer = message.SenderCustomer,
                CargoCompanyId = message.CargoCompanyId,
                CargoCustomerId = message.CargoCustomerId,
                VendorId = message.VendorId,
                Weight = message.Weight,
                Width = message.Width,
                Height = message.Height,
                Length = message.Length
                // ❌ Barkod ve Statü burada atanmaz! Manager halledecek.
            };

            // 🚀 TETİKLE: Tüm iş mantığı artık Manager'ın omuzlarında.
            await _cargoDetailService.TInsertAsync(createDto);

            Console.WriteLine($">>>>> [CONSUMER]: Sipariş {message.OrderingId} için Manager tetiklendi. <<<<<");
        }
    }
}