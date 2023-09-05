using mohoShop.Models;

namespace mohoShop.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts(int page);
        ICollection<Product> GetProductsByName(string ProductName);
        Product GetProductById(int productId);
        bool ProductExists(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        bool Save();
    }
}
