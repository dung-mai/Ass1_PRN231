using BusinessObject.DTO;

namespace Repositories.IRepository
{
    public interface ICategoryRepository
    {
        public List<CategoryDTO> GetCategories();
    }
}
