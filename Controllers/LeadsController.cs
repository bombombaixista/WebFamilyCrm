using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    public class LeadsController : Controller
    {
        public IActionResult Index()
        {
            // NÃO busca nada no banco
            // Os dados vêm do localStorage (JS)
            return View();
        }
    }
}
