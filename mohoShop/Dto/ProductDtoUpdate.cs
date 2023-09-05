namespace mohoShop.Dto
{
    public class ProductDtoUpdate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public double? DiscountPrice { get; set; }
        public string? Description { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile? FormFile { get; set; }

    }
}
