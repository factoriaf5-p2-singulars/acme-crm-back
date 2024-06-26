using System.Text.Json.Serialization;
using acme_crm.Customers;

namespace acme_crm.Product;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}