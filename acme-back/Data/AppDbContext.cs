using Microsoft.EntityFrameworkCore;

namespace acme_back.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product.Product> Products { get; set; }
}