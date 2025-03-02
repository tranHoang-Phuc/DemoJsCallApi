namespace DemoJsCallApi.Dtos
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
