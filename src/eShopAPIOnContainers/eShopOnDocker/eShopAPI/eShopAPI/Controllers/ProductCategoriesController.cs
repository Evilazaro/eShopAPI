using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopAPI.Data;
using eShopAPI.Data.Entities;
using eShopAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public ProductCategoriesController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetProductCategories()
        {
            return await _context.ProductCategories.Select(c=> new CategoryViewModel {
             Id=c.Id,
              Description=c.Description,
             Name=c.Name
            
            }).ToListAsync();
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetProductCategory(Guid id)
        {
            var productCategory = await _context.ProductCategories.Where(z => z.Id == id).Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Description = c.Description,
                Name = c.Name

            }).SingleOrDefaultAsync();

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryViewModel>> PutProductCategory(Guid id, CategoryUpdateModel data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != data.Id)
            {
                return BadRequest();
            }
            var category = _context.ProductCategories.Find(id);
            if (category == null) return NotFound();
            category.Description = data.Description;
            category.Name = data.Name;

            await _context.SaveChangesAsync();

            return await GetProductCategory(id);
        }

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> PostProductCategory(CategoryCreateModel data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newCategory = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = data.Description,
                Name = data.Name
            };
            _context.Add(newCategory);
            await _context.SaveChangesAsync();

            return await GetProductCategory(newCategory.Id);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(Guid id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
