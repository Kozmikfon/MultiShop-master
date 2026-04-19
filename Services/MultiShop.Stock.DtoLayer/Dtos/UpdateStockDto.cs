using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.DtoLayer.Dtos
{
    public class UpdateStockDto
    {
        public string ProductId { get; set; }
        public int Count { get; set; }
    }
}
