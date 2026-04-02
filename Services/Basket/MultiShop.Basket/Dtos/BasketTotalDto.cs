namespace MultiShop.Basket.Dtos
{
    public class BasketTotalDto
    {
        public BasketTotalDto()
        {
            BasketItems=new List<BasketItemDto>();
        }
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice { get => BasketItems != null ? BasketItems.Sum(x => x.Price * x.Quantity) : 0; set { } }
        public double TotalWeight { get => BasketItems != null ? BasketItems.Sum(x => x.Weight * x.Quantity) : 0; set { } }
    }
}