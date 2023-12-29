using eShopAPI.Data;
using eShopAPI.Data.Entities;
using eShopAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public ProductsController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
            return await _context.Products.Select(p => new ProductViewModel
            {
                CategoryId = p.ProductCategoryId,
                CategoryName = p.ProductCategory.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Id = p.Id
            }).ToListAsync();
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts2()
        {
            return await _context.Products.Select(p => new ProductViewModel
            {
                CategoryId = p.ProductCategoryId,
                CategoryName = p.ProductCategory.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Id = p.Id
            }).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(Guid id)
        {
            var product = await _context.Products.Where(z => z.Id == id).Select(p => new ProductViewModel
            {
                CategoryId = p.ProductCategoryId,
                CategoryName = p.ProductCategory.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Id = p.Id
            }).SingleOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductViewModel>> PutProduct(Guid id, ProductUpdateModel data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != data.Id)
            {
                return BadRequest();
            }
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            product.Description = data.Description;
            product.ProductCategoryId = data.CategoryId;
            product.Price = data.Price;
            product.Name = data.Name;

            await _context.SaveChangesAsync();

            return await GetProduct(id);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> PostProduct(ProductCreateModel data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Description = data.Description,
                Name = data.Name,
                Price = data.Price,
                ProductCategoryId = data.CategoryId
            };
            _context.Add(newProduct);
            await _context.SaveChangesAsync();

            return await GetProduct(newProduct.Id);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
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
