namespace mohoShop.Models
{
    public enum Status
    {
        Unpaid = 0,
        Paid = 1,
        Cancel = -1
    }

    public class Order
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime DateOrder { get; set; }
        public Status Status { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Customer Customer { get; set; }

    }
}
