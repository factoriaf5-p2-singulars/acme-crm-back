using acme_back.Data;
using acme_back.Uitilitis.Services;
using acme_crm.Customers;
using acme_crm.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace acme_crm.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    { 
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IFileStorage _fileStorage;

        public CustomerController(ApplicationDBContext context, IFileStorage fileStorage, ICustomerRepository customerRepository, IMapper mapper)
        {
            _context = context;
            _fileStorage = fileStorage;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomer();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>>  CreateCustomer(CreateCustomerDto createCustomerDto)
        {

            var customer = _mapper.Map<Customer>(createCustomerDto);
            if (createCustomerDto.Avatar != null)
            {
                string url = await _fileStorage.Storage("customer", createCustomerDto.Avatar);
                customer.Avatar = url;
            }

            await _customerRepository.CreateCustomer(customer);
            return Ok(customer);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] CreateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCustomerDto, customer);
            await _customerRepository.UpdateCustomer(customer);
            return NoContent();
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
