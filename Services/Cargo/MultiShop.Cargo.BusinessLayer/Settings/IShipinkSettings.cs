using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Settings
{
    public interface IShipinkSettings
    {
        public string BaseUrl { get; set; }
        public string WarehouseId { get; set; }
        public string CarrierAccountId { get; set; }
        public string CarrierServiceId { get; set; }

        string CardId { get; set; }
    }
}
