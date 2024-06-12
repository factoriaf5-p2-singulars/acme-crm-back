using acme_crm.Customers;
using acme_crm.Product;
using Microsoft.EntityFrameworkCore;

namespace acme_back.Data;

public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
}