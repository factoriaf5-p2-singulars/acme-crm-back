using acme_back.Data;
using acme_crm.Customers;
using Microsoft.EntityFrameworkCore;

namespace acme_crm.Customers;

public class CustomerRepository : ICustomerRepository
{
    
    private readonly ApplicationDBContext _context;

    public CustomerRepository (ApplicationDBContext context)
    {
       
        _context = context;
        
    }
    
    public async Task<IEnumerable<Customer>> GetAllCustomer()
    {
        return await _context.Customers.Include(x=>x.Product).ToListAsync();
    }

    public async Task<int> CreateCustomer(Customer customer)
    {
        _context.Add(customer);
    
        await _context.SaveChangesAsync();
        return customer.Id;
    }
    
    
    public async Task UpdateCustomer(Customer customer)
    {
     
        _context.Update(customer);
      
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistCustomer(int id)
    {
        return await _context.Customers.AnyAsync(x=>x.Id == id);
    }

    public async Task Delete(int id)
    {
        await _context.Customers.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
    
}