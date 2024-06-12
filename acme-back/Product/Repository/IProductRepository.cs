namespace acme_crm.Product;

public interface IProductRepository
{
    Task<List<Product>> GetAllProduct();
    Task<int> CreateProduct(Product createProductDto);
    Task UpdateProduct(Product product);
    Task<bool> ExistProduct(int id);
    Task Delete(int id);
}