using System.ComponentModel.DataAnnotations;

namespace acme_back.Product;

public class Product
{
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; } = null!;
    public string Description { get; set; }= null!;
    
}