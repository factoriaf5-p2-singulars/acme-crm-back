using acme_crm.Customers;
using acme_crm.Product;
using AutoMapper;

namespace acme_crm.Utils;

 
public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product.Product, ProductDto>();
        CreateMap<CreateProductDto, Product.Product>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();

    }
}