using mohoShop.Data;
using mohoShop.Helpers;
using mohoShop.Interfaces;
using mohoShop.Models;

namespace mohoShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly Paging _paging;

        public CategoryRepository(DataContext context, Paging paging)
        {
            _context = context;
            _paging = paging;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
        public bool CategoryNameExists(string name)
        {
            return _context.Categories
                .Any(c => c.Name.Trim().ToUpper() == name.Trim().ToUpper());
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id ==  categoryId);
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories(int page)
        {
            var categories = _paging.Paginate(_context.Categories.ToList(), page);
            return categories;
        }

        public ICollection<Category> GetCategoryByName(string categoryName)
        {
            var categories = _context.Categories.Where(c => c.Name.Contains(categoryName)).ToList();
            return categories;
        }


        public ICollection<Product> GetProducts(int categoryId)
        {
            var products = _context.Categories
            .Where(c => c.Id == categoryId)
            .SelectMany(c => c.Products) // Use SelectMany to flatten the nested collections
            .ToList();

            return products;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            var categoryFound = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            categoryFound.Name = category.Name;
            return Save();
        }
    }
}
