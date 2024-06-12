using acme_back.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace acme_crm.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerController:  ControllerBase
    { 
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repository;
        
        public customerController(ApplicationDBContext context, ICustomerRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _repository.GetAllCustomer();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }
        
        
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            var id = await _repository.CreateCustomer(customer);
           return Ok(customer);
        }
        
        [HttpPut("{id:int}")]
        public async Task<Results<NoContent,NotFound>> UpdateCustomer(int id, [FromBody]CustomerDto updateCustomerDto)
        {
            var exist = await _repository.ExistCustomer(id);

            if (!exist)
            {
                return TypedResults.NotFound();
            }
    
            var customer = _mapper.Map<Customer>(updateCustomerDto);
            customer.Id = id;

            await _repository.UpdateCustomer(customer);
            return TypedResults.NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
