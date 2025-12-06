using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Kanban.Data;
using Kanban.Models;

namespace Kanban.Controllers
{
    public class LoginController : Controller
    {
        private readonly KanbanContext _context;

        public LoginController(KanbanContext context)
        {
            _context = context;
        }

        // GET: /Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        public async Task<IActionResult> Index(string email, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.SenhaHash == senha);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Erro = "Login inválido";
            return View();
        }

        // GET: /Login/Cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: /Login/Cadastro
        [HttpPost]
        public IActionResult Cadastro(string username, string email, string senha)
        {
            var novoUsuario = new Usuario
            {
                Username = username,
                Email = email,
                SenhaHash = senha, // ⚠️ ideal usar hash seguro
                Role = "User",
                DataCriacao = DateTime.Now
            };

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }

        // GET: /Login/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
