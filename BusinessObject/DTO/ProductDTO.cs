using BusinessObject.Models;
using System;
using System.Text;

namespace BusinessObject.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public float Weight { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        //public CategoryDTO Category { get; set; } = null!;
    }
}
