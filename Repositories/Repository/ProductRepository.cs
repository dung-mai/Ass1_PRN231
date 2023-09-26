using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        MyDBContext _context;
        IMapper _mapper;

        public ProductRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductResponseDTO> GetProducts(int? categoryId = 0)
        {
            ProductDAO productDAO = new ProductDAO(_context);
            List<Product> products = productDAO.GetProducts(categoryId);
            return products.Select(p => _mapper.Map<ProductResponseDTO>(p)).ToList();
        }

        public List<ProductDTO> GetProducts(int? categoryId, int orderBy)
        {
            ProductDAO productDAO = new ProductDAO(_context);
            List<Product> products = productDAO.GetProducts(categoryId);
            if (orderBy == 2)
            {
                products = products.OrderByDescending(p => p.UnitPrice).ToList();
            }
            else
            {
                products = products.OrderBy(p => p.UnitPrice).ToList();
            }
            return products.Select(p => _mapper.Map<ProductDTO>(p)).ToList();
        }

        public ProductDTO? GetProduct(int id)
        {
            ProductDAO productDAO = new ProductDAO(_context);
            return _mapper.Map<ProductDTO>(productDAO.GetProduct(id));
        }

        public void UpdateUnitInStock(ProductDTO product, int quantity)
        {
            ProductDAO productDAO = new ProductDAO(_context);
            product.UnitsInStock -= (short)quantity;
            productDAO.UpdateProduct(_mapper.Map<Product>(product));
            _context.SaveChanges();
        }

        public bool SaveProduct(ProductDTO product)
        {
            try
            {
                ProductDAO productDAO = new ProductDAO(_context);
                int result = productDAO.AddProduct(_mapper.Map<Product>(product));
                if (result > 0)
                {
                _context.SaveChanges();
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public void UpdateProduct(ProductDTO product)
        {
            ProductDAO productDAO = new ProductDAO(_context);
            productDAO.UpdateProduct(_mapper.Map<Product>(product));
            _context.SaveChanges();
        }

        public bool DeleteProduct(ProductDTO tempProduct)
        {
            try
            {
                ProductDAO productDAO = new ProductDAO(_context);
                productDAO.DeleteProduct(_mapper.Map<Product>(tempProduct).ProductId);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
