using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    public class ClientesController : Controller
    {
        // SOMENTE abre a tela (não usa banco)
        public IActionResult Index()
        {
            return View();
        }
    }
}
