using MediatR;
using MultiShop.Order.Application.Dtos.ExternalDtos;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommand>
    {
        private readonly IRepository<Ordering> _orderingRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateOrderingCommandHandler(IRepository<Ordering> repository, IHttpClientFactory httpClientFactory, IRepository<Address> address)
        {
            _orderingRepository = repository;
            _httpClientFactory = httpClientFactory;
            _addressRepository = address;
        }

        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Siparişi Kendi Veritabanına Kaydet
            var ordering = new Ordering
            {
                OrderDate = request.OrderDate,
                TotalPrice = request.TotalPrice,
                UserId = request.UserId,
                AddressId = request.AddressId // Hangi adres seçildi?
            };
            await _orderingRepository.CreateAsync(ordering);

            // 2. ADIM: Seçilen Adresin Detaylarını Getir (Snapshot Alıyoruz)
            var address = await _addressRepository.GetByIdAsync(request.AddressId);

            if (address == null)
            {
                // Adres bulunamazsa kargo süreci başlayamaz. 
                // Burada log atılabilir ama throw yapmıyoruz ki sipariş yanmasın.
                return;
            }

            try
            {
                var client = _httpClientFactory.CreateClient();

                // 3. ADIM: Cargo Servisine Gidecek "İç Paket"i Hazırla
                // Bu DTO, bizim Cargo mikroservisindeki tablolara dolacak.
                var cargoDto = new CreateCargoDetailDto
                {
                    SenderCustomer = request.SenderCustomer, // Şirket adın
                    ReceiverName = $"{address.Name} {address.Surname}", // Adres tablosundan birleştirdik
                    ReceiverPhone = address.Phone,
                    ReceiverEmail = address.Email,
                    ReceiverAddress = $"{address.Detail1} {address.Detail2} {address.Description}",
                    ReceiverCity = address.City,
                    ReceiverDistrict = address.District,
                    Barcode = new Random().Next(100000, 999999),
                    CargoCompanyId = request.CargoCompanyId,
                    CargoCustomerId = request.CargoCustomerId,
                    VendorId = ordering.UserId,
                    OrderingId = ordering.OrderingId // Az önce kaydedilen siparişin ID'si
                };

                var jsonData = JsonSerializer.Serialize(cargoDto);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // 4. ADIM: Cargo Mikroservisine Veriyi Fırlat
                // Bu aşamada henüz Shipink devrede değil, sadece bizim Cargo servisine veriyi emanet ediyoruz.
                await client.PostAsync("http://localhost:7073/api/CargoDetails", content);
            }
            catch (Exception ex)
            {
                // Kargo servisi kapalıysa veya hata verirse sipariş yine de tamamlanmış oldu.
                // İleride buraya RabbitMQ gelecek ve bu 'try-catch'e gerek kalmayacak.
            }
        }
    }
}
