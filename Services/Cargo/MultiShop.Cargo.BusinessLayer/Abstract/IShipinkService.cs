using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface IShipinkService
    {
        // İlk kayıt anında Shipink'e gönderim
        Task<string> CreateShipmentAsync(int cargoDetailId);

        // Durum değişikliklerini Shipink'e bildirme
        Task<bool> UpdateStatusAsync(string shipinkId, string newStatus);
    }
}
