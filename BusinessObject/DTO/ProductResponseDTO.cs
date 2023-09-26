using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public float Weight { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public CategoryDTO Category { get; set; } = null!;
    }
}
