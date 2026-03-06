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
        private readonly IRepository<Ordering> _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateOrderingCommandHandler(IRepository<Ordering> repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {

            var ordering = new Ordering
            {
                OrderDate = request.OrderDate,
                TotalPrice = request.TotalPrice,
                UserId = request.UserId
            };

            await _repository.CreateAsync(ordering);

            var cargoDto = new CreateCargoDetailDto
            {
                SenderCustomer = request.SenderCustomer, // TAM DİNAMİK
                Barcode = new Random().Next(100000, 999999), // Sistem üretir
                CargoCompanyId = request.CargoCompanyId, // TAM DİNAMİK
                CargoCustomerId = request.CargoCustomerId, // TAM DİNAMİK
                VendorId = ordering.UserId, // TAM DİNAMİK
                OrderingId = ordering.OrderingId // TAM DİNAMİK
            };

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonSerializer.Serialize(cargoDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

   
            await client.PostAsync("http://localhost:7073/api/CargoDetails", content);
        }
    }
}
