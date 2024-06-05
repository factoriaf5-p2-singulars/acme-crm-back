using acme_back.Data;

namespace acme_back.Product;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _context;

    public ProductRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProduct(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description
        };
        _context.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }
}