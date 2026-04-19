using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Stock.BusinessLayer.Abstract;
using MultiShop.Stock.DtoLayer.Dtos;

namespace MultiShop.Stock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> StockList()
        {
            var values = await _stockService.TGetAllAsync();
            return Ok(values);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockByProductId(string id)
        {
            var values = await _stockService.TGetByProductIdAsync(id);
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(UpdateStockDto updateStockDto)
        {
            await _stockService.TUpdateAsync(updateStockDto);
            return Ok("Stok başarıyla güncellendi");
        }
    }
}
