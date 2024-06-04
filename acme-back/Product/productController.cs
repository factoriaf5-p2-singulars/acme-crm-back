using acme_back.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace acme_back.Product
{ 
    [Route("api/[controller]")] 
    [ApiController]
    public class productController : ControllerBase
    { 
        private readonly AppDbContext _context;
        
        public productController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<IProductDto>>> GetAllCategory()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
        
        
        [HttpGet ("{id:int}")]
        public async Task<ActionResult<List<IProductDto>>> GetIdCategory(int id)
        {
            var category = await _context.Products.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        
        [HttpPost]
        public async Task<ActionResult<IProductDto>> AddCategory(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody]Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.Products.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Products.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
