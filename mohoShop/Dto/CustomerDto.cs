using System.ComponentModel.DataAnnotations;

namespace mohoShop.Dto
{
    public class CustomerDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Gmail { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
