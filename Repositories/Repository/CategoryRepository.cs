using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using BusinessObject.DTO;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        MyDBContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetCategories()
        {
            CategoryDAO categoryDAO = new(_context);
            return categoryDAO.GetCategories().Select(c => _mapper.Map<CategoryDTO>(c)).ToList();
        }
    }
}
