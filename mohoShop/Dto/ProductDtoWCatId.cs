using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace mohoShop.Dto
{
    public class ProductDtoWCatId
    {
        [Required]
        public string Name { get; set; }
        [Required, Range(1, double.MaxValue)]
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? Thumbnail { get; set; }
        [JsonIgnore] // Exclude this property from serialization
        public DateTime CreateAt { get; set; }
        [JsonIgnore] // Exclude this property from serialization
        public DateTime UpdateAt { get; set; }
        public int CategoryId { get; set;}

        public IFormFile? FormFile { get; set; }
    }
}
