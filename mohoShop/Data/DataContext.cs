using Microsoft.EntityFrameworkCore;
using mohoShop.Models;

namespace mohoShop.Data
{
    public class DataContext : DbContext
    {
        // This constructor is used to set up the context and connect it to the database.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category) 
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}