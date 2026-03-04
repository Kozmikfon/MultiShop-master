using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System.Threading.Tasks;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values= await _cargoCompanyService.TGetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            await _cargoCompanyService.TInsertAsync(createCargoCompanyDto);
            return Ok("Kargo şirketi oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoCompany(int id)
        {
            await _cargoCompanyService.TDeleteAsync(id);
            return Ok("Kargo şirketi silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCargoCompany(int id) //GetCargoCompanyById
        {
            var value = await _cargoCompanyService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            await _cargoCompanyService.TUpdateAsync(updateCargoCompanyDto);
            return Ok("karg şirketi bilgisi güncellendi");
        }
    }
}
