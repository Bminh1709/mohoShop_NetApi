namespace mohoShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        // public int OrderId { get; set; }
        // public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        // Quantity * ProductPrice
        public double TotalPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
