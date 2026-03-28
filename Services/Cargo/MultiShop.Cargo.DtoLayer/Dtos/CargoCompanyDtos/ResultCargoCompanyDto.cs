using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos
{
    public class ResultCargoCompanyDto
    {
        public int CargoCompanyId { get; set; }
        public string CargoCompanyName { get; set; }
        public string CarrierAccountId { get; set; }
        public string CarrierServiceId { get; set; }
    }
}
