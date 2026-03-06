using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System.Threading.Tasks;

namespace MultiShop.Cargo.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _CargoDetailService;
        public CargoDetailsController(ICargoDetailService CargoDetailService)
        {
            _CargoDetailService = CargoDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoDetailList()
        {
            var values =await _CargoDetailService.TGetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoDetail(CreateCargoDetailDto createCargoDetailDto)
        {
            
            await _CargoDetailService.TInsertAsync(createCargoDetailDto);
            return Ok("Kargo Detayları Başarıyla Oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoDetail(int id)
        {
            await _CargoDetailService.TDeleteAsync(id);
            return Ok("Kargo Detayları Başarıyla Silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoDetailById(int id)
        {
            var values = await _CargoDetailService.TGetByIdAsync(id);
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
        {
            
            await _CargoDetailService.TUpdateAsync(updateCargoDetailDto);
            return Ok("Kargo Detayları Başarıyla Güncellendi");
        }

        [HttpGet("GetCargoDetailByVendorId")]
        public async Task<IActionResult> GetCargoDetailByVendorId(string id)
        {
            var values =await _CargoDetailService.TGetCargoDetailsByVendorId(id);
            return Ok(values);
        }
        [HttpGet("ChangeCargoStatus")]
        public async Task<IActionResult> ChangeCargoStatus(int id,CargoStatus status)
        {
            var values= await _CargoDetailService.TChangeCargoStatus(id, status);
            return Ok(values);
        }
    }
}