namespace mohoShop.Models
{
    public class Responsibility
    {
        public int Id { get; set; }
        // public int RoleId { get; set; }
        // public int AdminId { get; set; }
        public DateTime CreateAt { get; set; }
        public Role Role { get; set; }
        public Admin Admin { get; set; }

    }
}
