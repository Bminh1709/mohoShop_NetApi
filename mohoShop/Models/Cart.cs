﻿namespace mohoShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        // public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
