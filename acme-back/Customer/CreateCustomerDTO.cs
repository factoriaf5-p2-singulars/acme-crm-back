namespace acme_crm.Customers;

public class CreateCustomerDto
{
    public string Name { get; set; } = null!;
    public DateTime EntryDate { get; set; } 
    public IFormFile? Avatar { get; set; }
}