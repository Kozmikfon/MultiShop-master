using AutoMapper;
using MultiShop.Stock.DtoLayer.Dtos;
using MultiShop.Stock.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.BusinessLayer.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            // StockDetail <-> ResultStockDto (Çift yönlü)
            CreateMap<EStock, ResultStockDto>().ReverseMap();

            // UpdateStockDto -> StockDetail
            CreateMap<UpdateStockDto, EStock>().ReverseMap();
        }
    }
}
