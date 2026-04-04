using MultiShop.Shared.Events.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.Concrete
{
    public class OrderTrackingNumberCreatedEvent : IOrderTrackingNumberCreatedEvent
    {
        public int OrderingId { get; set; }

        public string TrackingNumber { get; set; }
    }
}
