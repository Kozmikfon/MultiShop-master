using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
    public class ShipinkManager : IShipinkService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShipinkManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> CreateOrderAsync(CreateOrderingCommand command, Address address)
        {
            var token = await GetAccessTokenAsync();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 1. Ürün listesini Shipink formatına (items) çeviriyoruz
            var shipinkItems = command.OrderDetails.Select(x => new
            {
                name = x.ProductName,
                quantity = x.ProductAmount,
                price = (double)x.ProductPrice,
                origin = "TR"
            }).ToList();

            // 2. Shipink Sipariş JSON yapısı
            var orderRequest = new
            {
                customer = new
                {
                    name = $"{address.Name} {address.Surname}",
                    email = new { main = address.Email },
                    phone = new { main = address.Phone },
                    address = new
                    {
                        street = address.Detail1,
                        city = address.City,
                        state = address.District,
                        country_code = "TR"
                    }
                },
                items = shipinkItems,
                currency = "TRY",
                price = (double)command.TotalPrice,
                payment = new
                {
                    method = "credit-card",
                    status = "completed"
                }
            };

            // 3. /orders endpoint'ine POST atıyoruz
            var response = await client.PostAsJsonAsync("https://api.dev.shipink.io/orders", orderRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                // data -> id yolundan ID'yi çekiyoruz
                return result.GetProperty("data").GetProperty("id").GetString();
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Shipink Sipariş Hatası: {error}");
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var loginData = new { username = "baglamamahmut9@gmail.com", password = "Kozshipink0." };
            var response = await client.PostAsJsonAsync("https://api.dev.shipink.io/token", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                return result.GetProperty("access_token").GetString();
            }
            return null;
        }
    }
}
