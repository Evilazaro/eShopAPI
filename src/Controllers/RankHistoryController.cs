using eShopAPI.Data;
using eShopAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShopAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RankHistoryController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public RankHistoryController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/RankHistory/Product/Guid
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<RankHistoryViewModel>>> GetRankHistory([FromRoute]Guid productId)
        {
            return await _context.RankHistory.Where(z=>z.ProductId== productId).Select(rank=>new RankHistoryViewModel { 
             Id=rank.Id,
             Date=rank.Date,
             Rank=rank.Rank, 
             ProductId=rank.ProductId,
             ProductName=rank.Product.Name
            }).OrderByDescending(z=>z.Date).ToListAsync();
        }


    }
}
