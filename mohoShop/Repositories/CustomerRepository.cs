using mohoShop.Data;
using mohoShop.Helpers;
using mohoShop.Interfaces;
using mohoShop.Models;

namespace mohoShop.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public bool SignUp(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool AccountExists(string gmail)
        {
            return _context.Customers.Any(c => c.Gmail == gmail);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public string GetPassword(string gmail)
        {
            var foundCustomer = _context.Customers.FirstOrDefault(c => c.Gmail == gmail);
            return foundCustomer.Password;
        }

        public Customer GetCustomer(string gmail)
        {
            return _context.Customers.FirstOrDefault(c => c.Gmail == gmail);
        }
    }
}
