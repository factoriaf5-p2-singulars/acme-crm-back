namespace acme_back.Product;

public interface IProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}