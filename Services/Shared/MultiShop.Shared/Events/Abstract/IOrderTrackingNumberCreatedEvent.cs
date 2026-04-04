using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Abstract
{
    public interface IOrderTrackingNumberCreatedEvent
    {
        int OrderingId { get; }
        string TrackingNumber { get; }
    }
}
