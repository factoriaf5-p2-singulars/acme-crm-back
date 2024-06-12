using acme_crm.Product;

namespace acme_crm.Customers;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime EntryDate { get; set; } 
    
    public List<ProductDto> Product { get; set; }
}