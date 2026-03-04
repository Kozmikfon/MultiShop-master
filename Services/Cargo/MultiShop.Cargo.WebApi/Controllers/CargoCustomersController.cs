using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System.Threading.Tasks;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCustomerList()
        {
            var values = await _cargoCustomerService.TGetAllAsync();
            return Ok(values);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCustomerById(int id)
        {
            var value=await _cargoCustomerService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            await _cargoCustomerService.TInsertAsync(createCargoCustomerDto);
            return Ok("kargo müşteri bilgisi eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoCustomer(int id)
        {
            await _cargoCustomerService.TDeleteAsync(id);
            return Ok("Kargo Müşteri Silme İşlemi Başarıyla Yapıldı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            
            await _cargoCustomerService.TUpdateAsync(updateCargoCustomerDto);
            return Ok("Kargo Müşteri Güncelleme İşlemi Başarıyla Yapıldı");
        }

        //[HttpGet("GetCargoCustomerById")] 
        //public async Task<IActionResult> GetCargoCustomerById(string id)
        //{
        //    var value = await _cargoCustomerService.TGetByIdAsync(id); 
        //    return Ok(value);
        //}
    }
}
