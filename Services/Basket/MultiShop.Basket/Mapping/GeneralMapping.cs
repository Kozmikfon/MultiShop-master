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
            CreateMap<BasketCheckoutDto, BasketCheckoutEvent>().ReverseMap();
            CreateMap<BasketTotalDto, BasketCheckoutEvent>().ReverseMap();


        }
    }
}
