using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoCompany
    {
        public string CargoCompanyId { get; set; }

        public string CarrierAccountId { get; set; }
        public string CarrierServiceId { get; set; }
        public string CargoCompanyName { get; set; }
    }
}
