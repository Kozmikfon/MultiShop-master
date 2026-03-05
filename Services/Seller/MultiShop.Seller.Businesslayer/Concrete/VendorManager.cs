using AutoMapper;
using MultiShop.Seller.Businesslayer.Abstract;
using MultiShop.Seller.DataAccessLayer.Abstract;
using MultiShop.Seller.DtoLayer.Dtos.VendorDtos;
using MultiShop.Seller.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.Businesslayer.Concrete
{
    public class VendorManager : IVendorService
    {
        private readonly IMapper _mapper;
        private readonly IVendorDal _vendorDal;
        public VendorManager(IVendorDal vendorDal, IMapper mapper)
        {
            _vendorDal = vendorDal;
            _mapper = mapper;
        }
        public async Task TDeleteAsync(int id)
        {
           await _vendorDal.DeleteAsync(id);
        }

        public async Task<List<ResultVendorDto>> TGetAllAsync()
        {
           var values=await _vendorDal.GetAllAsync();
            return _mapper.Map<List<ResultVendorDto>>(values);
        }

        public async Task<GetByIdVendorDto> TGetByIdAsync(int id)
        {
            var value=await _vendorDal.GetByIdAsync(id);
            return _mapper.Map<GetByIdVendorDto>(value);
        }

        public async Task TInsertAsync(CreateVendorDto createVendorDto)
        {
            var value=_mapper.Map<Vendor>(createVendorDto);
            await _vendorDal.InsertAsync(value);
        }

        public async Task TUpdateAsync(UpdateVendorDto updateVendorDto)
        {
            var value=_mapper.Map<Vendor>(updateVendorDto);
            await _vendorDal.UpdateAsync(value);
        }
    }
}
