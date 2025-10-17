using eShopAPI.Data;
using eShopAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PriceHistoryController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public PriceHistoryController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/PriceHistory/Product/Guid
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<PriceHistoryViewModel>>> GetRankHistory([FromRoute] Guid productId)
        {
            return await _context.PriceHistory.Where(z => z.ProductId == productId).Select(price => new PriceHistoryViewModel
            {
                Id = price.Id,
                NewPrice = price.NewPrice,
                ChangeDate = price.ChangeDate,
                ProductId = price.ProductId,
                ProductName = price.Product.Name
            }).OrderByDescending(z => z.ChangeDate).ToListAsync();
        }


    }
}
