using Kanban.Data;
using Kanban.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Controllers
{
    public class LoginController : Controller
    {
        private readonly SalesPipelineContext _context;

        public LoginController(SalesPipelineContext context)
        {
            _context = context;
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
                ViewBag.Error = "Informe usuário e senha";
                return View();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == nome);

            if (user == null || user.PasswordHash != senha)
            {
                ViewBag.Error = "Usuário ou senha inválidos";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim("UserId", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return RedirectToAction("Index", "Leads");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
