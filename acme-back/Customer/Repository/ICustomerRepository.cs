namespace acme_crm.Customers;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomer();
    Task<int> CreateCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task<bool> ExistCustomer(int id);
    Task Delete(int id);
}