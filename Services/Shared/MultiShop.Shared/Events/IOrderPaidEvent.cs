using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public interface IOrderPaidEvent
    {
        int OrderingId { get; }
        string ReceiverName { get; }
        string ReceiverSurname { get; }
        string ReceiverEmail { get; }
        string ReceiverPhone { get; }
        string ReceiverCity { get; }
        string ReceiverDistrict { get; }
        string ReceiverAddressDetail { get; }
        // Shipink için lazım olacak kargo firması ID'si
        int CargoCompanyId { get; }
        int CargoCustomerId { get; }

        double Weight { get; }
        int Width { get; }
        int Height { get; }
        int Length { get; }
    }
}
