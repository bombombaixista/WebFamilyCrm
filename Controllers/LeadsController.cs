using Kanban.Data;
using Kanban.Models;
using Kanban.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Controllers
{
    public class LeadsController : Controller
    {
        private readonly SalesPipelineContext _context;

        public LeadsController(SalesPipelineContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new PipelineViewModel
            {
                Stages = await _context.Stages.OrderBy(s => s.OrderIndex).ToListAsync(),
                Deals = await _context.Deals.ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeal([FromBody] Deal newDeal)
        {
            _context.Deals.Add(newDeal);
            await _context.SaveChangesAsync();
            return Json(new { success = true, id = newDeal.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDeal([FromBody] DeleteDealRequest request)
        {
            var deal = await _context.Deals.FindAsync(request.DealId);
            if (deal == null) return NotFound();

            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> MoveDeal([FromBody] MoveDealRequest request)
        {
            var deal = await _context.Deals.FindAsync(request.DealId);
            if (deal == null) return NotFound();

            deal.StageId = request.NewStageId;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }

    public class DeleteDealRequest { public int DealId { get; set; } }
    public class MoveDealRequest { public int DealId { get; set; } public int NewStageId { get; set; } }
}
