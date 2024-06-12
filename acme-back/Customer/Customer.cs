using System.Text.Json.Serialization;
using acme_crm.Customers;

namespace acme_crm.Customers;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime EntryDate { get; set; }
    
}