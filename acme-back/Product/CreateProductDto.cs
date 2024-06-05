namespace acme_back.Product;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; } = null;
}