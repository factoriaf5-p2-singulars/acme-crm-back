using acme_back.Data;
using acme_crm.Customers;
using acme_crm.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace acme_crm.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    { 
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IFileStorage _fileStorage;
        
        public productController(ApplicationDBContext context,IFileStorage _fileStorage, IProductRepository productRepository, IMapper mapper)
        {
            _context = context;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProduct()
        {
            var products = await _productRepository.GetAllProduct();
            _mapper.Map<List<ProductDto>>(products);
            return Ok(products);
        }
        
        
        [HttpGet ("{id:int}")]
        public async Task<ActionResult<List<ProductDto>>> GetIdProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(int customerId,CreateProductDto createProductDto, ICustomerRepository customerRepository)
        {
            var product = _mapper.Map<Product>(createProductDto);
            if(createProductDto.Photo != null)
            {
                string url = await _fileStorage.Storage(container:"product", createProductDto.Photo);
                product.Photo = url;
            }
            product.CustomerId = customerId;
            await _productRepository.CreateProduct(product);
            return Ok(product);
        }

        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct([FromBody]CreateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
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
