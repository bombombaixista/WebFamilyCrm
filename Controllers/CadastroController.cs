using Kanban.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ApiService _apiService;

        public CadastroController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nome, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Error = "Nome e senha são obrigatórios.";
                return View();
            }

            var sucesso = await _apiService.RegisterAsync(nome, senha);

            if (!sucesso)
            {
                ViewBag.Error = "Erro ao cadastrar usuário.";
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
