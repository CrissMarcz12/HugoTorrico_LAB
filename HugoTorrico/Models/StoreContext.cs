using Microsoft.EntityFrameworkCore;

namespace HugoTorrico.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; } 

        public DbSet<Product> Products { get; set; }

        public DbSet<Detail> Details { get; set; }



    }
}
