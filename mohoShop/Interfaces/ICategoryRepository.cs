using mohoShop.Models;

namespace mohoShop.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories(int page);
        ICollection<Category> GetCategoryByName(string categoryName);
        ICollection<Product> GetProducts(int categoryId);
        Category GetCategoryById(int categoryId);
        bool CategoryExists(int id);
        bool CategoryNameExists(string name);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);
        bool Save();

    }
}
