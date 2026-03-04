using AutoMapper;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;
        private readonly IMapper _mapper;
        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal, IMapper mapper)
        {
            _cargoCustomerDal = cargoCustomerDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _cargoCustomerDal.Delete(id);
        }

        public async Task<List<ResultCargoCustomerDto>> TGetAllAsync()
        {
            var value=await _cargoCustomerDal.GetAll();
            return _mapper.Map<List<ResultCargoCustomerDto>>(value);
        }

        public async Task<GetByIdCargoCustomerDto> TGetByIdAsync(int id)
        {
            var value=await _cargoCustomerDal.GetById(id);
            return _mapper.Map<GetByIdCargoCustomerDto>(value);
        }

        public async Task<CargoCustomer> TGetCargoCustomerById(string id)
        {
            var value=await _cargoCustomerDal.GetCargoCustomerById(id);
            return _mapper.Map<CargoCustomer>(value);
        }

        public async Task TInsertAsync(CreateCargoCustomerDto createDto)
        {
           var value=_mapper.Map<CargoCustomer>(createDto);
           await _cargoCustomerDal.Insert(value);
        }

        public async Task TUpdateAsync(UpdateCargoCustomerDto updateDto)
        {
           var value=_mapper.Map<CargoCustomer>(updateDto);
            await _cargoCustomerDal.Update(value);
        }
    }
}