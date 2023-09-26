using DataAccess.DAO;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Repositories.IRepository
{
    public interface IProductRepository
    {
        List<ProductResponseDTO> GetProducts(int? categoryId = ConstantValues.DEFAULT_CATEGORY);
        List<ProductDTO> GetProducts(int? categoryId, int orderBy);
        ProductDTO? GetProduct(int id);
        void UpdateUnitInStock(ProductDTO product, int quantity);
        void UpdateProduct(ProductDTO product);
        bool SaveProduct(ProductDTO product);
        bool DeleteProduct(ProductDTO tempProduct);
    }
}
