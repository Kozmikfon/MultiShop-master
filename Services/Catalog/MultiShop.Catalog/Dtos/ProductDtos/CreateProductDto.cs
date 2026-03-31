namespace MultiShop.Catalog.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryId { get; set; }
        public string VendorId { get; set; }
        public double Weight { get; set; } 
        public int Width { get; set; }     
        public int Height { get; set; }    
        public int Length { get; set; } 
    }
}
