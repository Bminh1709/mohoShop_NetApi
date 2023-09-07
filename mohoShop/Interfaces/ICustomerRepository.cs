using mohoShop.Models;

namespace mohoShop.Interfaces
{
    public interface ICustomerRepository
    {
        bool AccountExists(string gmail);
        bool SignUp(Customer customer);
        string GetPassword(string gmail);
        bool Save();
        Customer GetCustomer(string gmail);
    }
}
