namespace acme_back.Customer;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime EntryDate { get; set; } 
}