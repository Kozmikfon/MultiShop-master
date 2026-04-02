using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Abstract
{
    public interface IOrderCreatedEvent
    {
        int OrderingId { get; }
        string UserId { get; }
        decimal TotalPrice { get; }

        // Kargo kaydı için gereken adres bilgileri
        string ReceiverName { get; }
        string ReceiverSurname { get; }
        string ReceiverEmail { get; }
        string ReceiverPhone { get; }
        string ReceiverCity { get; }
        string ReceiverDistrict { get; }
        string ReceiverAddressDetail { get; }
        int CargoCompanyId { get; } // Bunu ekle
        int CargoCustomerId { get; }
        string VendorId { get; }
        string SenderCustomer { get; }

        double Weight { get; }      // Bunu ekle
        int Width { get; }         // Bunu ekle
        int Height { get; }        // Bunu ekle
        int Length { get; }        // Bunu ekle

        // Shipink tarafında önceden oluşmuş olan ID
        string ShipinkOrderId { get; }
    }
}
