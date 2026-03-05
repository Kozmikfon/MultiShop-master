using AutoMapper;
using MultiShop.Seller.DtoLayer.Dtos.VendorDtos;
using MultiShop.Seller.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.Businesslayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Vendor, ResultVendorDto>().ReverseMap();
            CreateMap<Vendor, CreateVendorDto>().ReverseMap();
            CreateMap<Vendor, UpdateVendorDto>().ReverseMap();
            CreateMap<Vendor, GetByIdVendorDto>().ReverseMap();
        }
    }
}
