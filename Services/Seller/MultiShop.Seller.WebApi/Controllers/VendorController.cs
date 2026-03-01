using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Seller.Businesslayer.Abstract;
using MultiShop.Seller.DataAccessLayer.Abstract;
using MultiShop.Seller.DtoLayer.Dtos.VendorDtos;
using MultiShop.Seller.EntityLayer.Entities;

namespace MultiShop.Seller.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> VendorList()
        {
            var values = await _vendorService.TGetAllAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVendor(CreateVendorDto createVendorDto)
        {
            await _vendorService.TInsertAsync(createVendorDto);
            return Ok("Vendor Başarıyla Oluşturuldu");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdVendor(int id)
        {
            var value = await _vendorService.TGetByIdAsync(id);
            return Ok(value);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveVendor(int id)
        {
            await _vendorService.TDeleteAsync(id);
            return Ok("Vendor Başarıyla Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVendor(UpdateVendorDto updateVendorDto)
        {
            await _vendorService.TUpdateAsync(updateVendorDto);
            return Ok("Vendor Başarıyla Güncellendi");

        }
    }
}
