namespace MultiShop.Seller.Entities
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Shopname { get; set; }
        public string TaxNumber { get; set; }
        public string SubMerchantKey { get; set; }
        public bool IsActive { get; set; }
    }
}
