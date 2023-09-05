using System.ComponentModel.DataAnnotations;

namespace mohoShop.Dto
{
    public class CategoryDtoNoId
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
