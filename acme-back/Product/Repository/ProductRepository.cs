using acme_back.Data;
using Microsoft.EntityFrameworkCore;

namespace acme_crm.Product;

public class ProductRepository : IProductRepository
{
    
    private readonly ApplicationDBContext _context;

    public ProductRepository(ApplicationDBContext context)
    {
        _context = context;
        
    }

    public async Task<List<Product>> GetAllProduct()
    {
        return await _context.Products.ToListAsync();

    }
    

    public async Task<int> CreateProduct(Product productDto)
    {
        _context.Add(productDto);
        await _context.SaveChangesAsync();
        return productDto.Id;
    }
    
    public async Task UpdateProduct(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistProduct(int id)
    {
        return await _context.Products.AnyAsync(x=>x.Id == id);
    }

    public async Task Delete(int id)
    {
        await _context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
    
}