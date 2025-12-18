using Kanban.Data;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealsController : Controller
    {
        private readonly SalesPipelineContext _context;

        public DealsController(SalesPipelineContext context)
        {
            _context = context;
        }

        [HttpPost("Move")]
        public async Task<IActionResult> Move([FromBody] MoveDealDto dto)
        {
            var deal = await _context.Deals.FindAsync(dto.DealId);
            if (deal == null)
                return NotFound();

            deal.StageId = dto.StageId;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

    public class MoveDealDto
    {
        public int DealId { get; set; }
        public int StageId { get; set; }
    }
}
