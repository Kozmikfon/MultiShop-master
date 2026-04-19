using MultiShop.Stock.DtoLayer.Dtos;
using MultiShop.Stock.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.BusinessLayer.Abstract
{
    public interface IStockService
    {
        Task TInsertAsync(EStock entity);
        Task TUpdateAsync(UpdateStockDto updateStockDto); // DTO alıp güncelleyecek
        Task<List<ResultStockDto>> TGetAllAsync(); // DTO dönecek
        Task<ResultStockDto> TGetByProductIdAsync(string productId);
    }
}
    
