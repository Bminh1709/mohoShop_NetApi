using System.ComponentModel.DataAnnotations;

namespace mohoShop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Gmail { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
