using acme_back.Data;
using acme_back.Uitilitis.Services;
using acme_crm.Customers;
using acme_crm.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace acme_crm.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IFileStorage _fileStorage;
        private readonly ICustomerRepository _customerRepository;

        public ProductController(ApplicationDBContext context, IFileStorage fileStorage, IProductRepository productRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _context = context;
            _fileStorage = fileStorage;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProduct()
        {
            var products = await _productRepository.GetAllProduct();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetIdProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(int customerId, CreateProductDto createProductDto)
        {

            var product = _mapper.Map<Product>(createProductDto);
            if (createProductDto.Photo != null)
            {
                string url = await _fileStorage.Storage("product", createProductDto.Photo);
                product.Photo = url;
            }

            product.CustomerId = customerId;
            await _productRepository.CreateProduct(product);
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] CreateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(updateProductDto, product);
            await _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
