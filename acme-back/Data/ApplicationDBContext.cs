using Microsoft.EntityFrameworkCore;

namespace acme_back.Data;

public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product.Product> Products { get; set; }
    public DbSet<Customer.Customer> Customers { get; set; }
    
}