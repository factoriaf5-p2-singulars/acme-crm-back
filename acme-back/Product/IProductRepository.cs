namespace acme_back.Product;

public interface IProductRepository
{
    Task<Product> CreateProduct(CreateProductDto createProductDto);
}