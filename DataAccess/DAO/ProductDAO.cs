using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        MyDBContext _context;
        public ProductDAO(MyDBContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                return _context.Products
                                .Include(p => p.Category)
                                .Where(p => p.UnitsInStock > 0).ToList();
            }
            return _context.Products
                            .Include(p => p.Category)
                            .Where(p => p.CategoryId == categoryId && p.UnitsInStock > 0).ToList();
        }

        public Product? GetProduct(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);
        }

        public int AddProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                return 1;
            }
            return 0;
        }

        public int DeleteProduct(int productId)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                return 1;
            }
            return 0;
        }

        public int UpdateProduct(Product _product)
        {
            Product? product = _context.Products
                .FirstOrDefault(o => o.ProductId == _product.ProductId);
            if (product != null)
            {
                product.ProductName = _product.ProductName;
                product.UnitPrice = _product.UnitPrice;
                product.CategoryId = _product.CategoryId;
                product.UnitsInStock = _product.UnitsInStock;
                return 1;
            }
            return 0;
        }
    }
}
