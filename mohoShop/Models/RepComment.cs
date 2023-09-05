namespace mohoShop.Models
{
    public class RepComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Boolean Status { get; set; }
        public Comment Comment { get; set; }
    }
}
