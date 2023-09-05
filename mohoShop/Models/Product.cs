using System.ComponentModel.DataAnnotations;

namespace mohoShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        // public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }




        // Relationship
        public Category? Category { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
