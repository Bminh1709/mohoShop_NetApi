using mohoShop.Data;
using mohoShop.Helpers;
using mohoShop.Interfaces;
using mohoShop.Models;

namespace mohoShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly Paging _paging;

        public ProductRepository(DataContext context, Paging paging)
        {
            _context = context;
            _paging = paging;
        }

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            _context.Products.Remove(product);
            return Save();
        }

        public ICollection<Product> GetProducts(int page)
        {
            var products = _paging.Paginate(_context.Products.ToList(), page);
            return products;
        }

        public ICollection<Product> GetProductsByName(string ProductName)
        {
            return _context.Products.Where(p => p.Name.Contains(ProductName)).ToList();
        }

        public Product GetProductById(int productId) 
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);  
        }

        public bool UpdateProduct(Product product)
        {
            var updateProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            if (updateProduct == null)
                return false;
            if (product.Name != null)
                updateProduct.Name = product.Name;
            if (product.Description != null)
                updateProduct.Description = product.Description;
            if (product.Price != updateProduct.Price && product.Price != 0)
                updateProduct.Price = product.Price;
            if (product.DiscountPrice != updateProduct.DiscountPrice && product.DiscountPrice != 0)
                updateProduct.DiscountPrice = product.DiscountPrice;
            if (product.Thumbnail != null)
                updateProduct.Thumbnail = product.Thumbnail;
            if (product.Category != null)
                updateProduct.Category = product.Category;
                
            updateProduct.UpdateAt = product.UpdateAt;
            
            _context.Update(updateProduct);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
