using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System.Threading.Tasks;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _CargoOperationService;
        public CargoOperationsController(ICargoOperationService CargoOperationService)
        {
            _CargoOperationService = CargoOperationService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoOperationList()
        {
            var values =await _CargoOperationService.TGetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
            
            await _CargoOperationService.TInsertAsync(createCargoOperationDto);
            return Ok("Kargo İşlemi Başarıyla Oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoOperation(int id)
        {
            await _CargoOperationService.TDeleteAsync(id);
            return Ok("Kargo İşlemi Başarıyla Silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoOperationById(int id)
        {
            var values =await _CargoOperationService.TGetByIdAsync(id);
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
            
            await _CargoOperationService.TUpdateAsync(updateCargoOperationDto);
            return Ok("Kargo İşlemi Başarıyla Güncellendi");
        }
    }
}
