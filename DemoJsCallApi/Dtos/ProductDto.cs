﻿namespace DemoJsCallApi.Dtos
{
    public class ProductDto
    {
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
    }
}
