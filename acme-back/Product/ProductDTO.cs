using acme_crm.Customers;

namespace acme_crm.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Photo { get; set; }
    
}