namespace mohoShop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Boolean Status { get; set; }


        // Relationship
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public ICollection<RepComment> RepComments { get; set; }
    }
}
