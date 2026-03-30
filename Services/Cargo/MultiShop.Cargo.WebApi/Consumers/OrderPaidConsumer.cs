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
            Console.WriteLine(">>>>>>>>> MESAJ CARGO SERVİSİNE ULAŞTI! <<<<<<<<<");
            Console.WriteLine($"Sipariş ID: {context.Message.OrderingId}");
            // 1. Gelen mesajı kargo nesnesine çevir (Mapping)
            var cargoDetail = new CargoDetail
            {
                // Temel Bilgiler
                OrderingId = context.Message.OrderingId,
                VendorId = "3" ?? "VND-DEFAULT", // SQL Hatasını önler
                SenderCustomer = "MultiShop Genel Depo",            // SQL Hatasını önler
                Barcode = $"BRK-{context.Message.OrderingId}-{DateTime.Now.Ticks.ToString().Substring(12)}",

                // Alıcı Bilgileri (Hata veren yerler buralardı)
                ReceiverName = context.Message.ReceiverName,
                ReceiverSurname = context.Message.ReceiverSurname,
                ReceiverEmail = context.Message.ReceiverEmail ?? "musteri@mail.com",
                ReceiverPhone = context.Message.ReceiverPhone ?? "05550000000",
                ReceiverCity = context.Message.ReceiverCity ?? "Ankara",
                ReceiverDistrict = context.Message.ReceiverDistrict ?? "Merkez",
                ReceiverAddressDetail = context.Message.ReceiverAddressDetail ?? "Adres Bilgisi Yok",

                // Boyut Bilgileri (Shipink'in istediği kısımlar)
                Weight = 1.0,
                Width = 15,
                Height = 10,
                Length = 20,

                // Durum ve Şirket
                CargoCompanyId = 1, // PTT vb.
                CargoCustomerId = 1,
                CurrentStatus = CargoStatus.Created
            };

            try
            {
                // 2. Önce Kargo DB'sine kaydet
                await _cargoDetaildal.Insert(cargoDetail);
                Console.WriteLine($"✅ Adım 1: Kargo DB kaydı başarılı. ID: {cargoDetail.CargoDetailId}");

                // 3. Shipink Sürecini Başlat (Daha önce yazdığımız o temiz Manager metodunu çağır)
                var result = await _shipinkService.CreateShipmentAsync(cargoDetail.CargoDetailId);
                Console.WriteLine($"🚀 Adım 2: Shipink Otomasyonu Tamamlandı: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hata: Otomasyon zinciri kırıldı! {ex.Message}");
                throw; // RabbitMQ mesajı hata kuyruğuna atsın
            }
        }
    }
}