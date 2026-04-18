using AutoMapper;
using MultiShop.Basket.Dtos;
using MultiShop.Shared.Events.Abstract;
using MultiShop.Shared.Events.Concrete;

namespace MultiShop.Basket.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<BasketItemDto, BasketItemEvent>().ReverseMap();

            // 🎯 DTO'dan Event'e geçerken SADECE kişisel bilgileri alalım (Fiyat/Ağırlık ezilmesin)
            CreateMap<BasketCheckoutDto, BasketCheckoutEvent>()
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())   // DTO'daki boş fiyat event'i bozmasın
                .ForMember(dest => dest.TotalWeight, opt => opt.Ignore())  // DTO'daki boş ağırlık event'i bozmasın
                .ReverseMap();

            // 🎯 Toplam Sepetten Event'e (Ürünler, Fiyat ve Ağırlık buradan gelir)
            CreateMap<BasketTotalDto, BasketCheckoutEvent>()
                .ForMember(dest => dest.BasketItems, opt => opt.MapFrom(src => src.BasketItems))
                .ReverseMap();


        }
    }
}
