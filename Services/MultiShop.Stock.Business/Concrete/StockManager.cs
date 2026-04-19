using AutoMapper;
using MultiShop.Stock.BusinessLayer.Abstract;
using MultiShop.Stock.DataAccessLayer.Abstract;
using MultiShop.Stock.DtoLayer.Dtos;
using MultiShop.Stock.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.BusinessLayer.Concrete
{
    
    public class StockManager : IStockService
    {
        private readonly IStockDal _stockDal;
        private readonly IMapper _mapper;

        public StockManager(IMapper mapper, IStockDal stockDal)
        {
            _mapper = mapper;
            _stockDal = stockDal;
        }

        public async Task TInsertAsync(EStock entity) => await _stockDal.InsertAsync(entity);

        public async Task TUpdateAsync(UpdateStockDto updateStockDto)
        {

            var values = await _stockDal.GetByFilterAsync(x => x.ProductId == updateStockDto.ProductId);

            if (values != null)
            {
                values.Count = updateStockDto.Count;
                await _stockDal.UpdateAsync(x => x.ProductId == updateStockDto.ProductId, values);
            }
        }

        public async Task<List<ResultStockDto>> TGetAllAsync()
        {
            var values = await _stockDal.GetAllAsync();
            return _mapper.Map<List<ResultStockDto>>(values);
        }

        public async Task<ResultStockDto> TGetByProductIdAsync(string productId)
        {
            var values = await _stockDal.GetByFilterAsync(x => x.ProductId == productId);
            return _mapper.Map<ResultStockDto>(values);
        }
    }
}
