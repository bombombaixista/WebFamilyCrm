using Microsoft.EntityFrameworkCore;
using Kanban.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com MySQL
builder.Services.AddDbContext<KanbanContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Adiciona suporte a controllers e views
builder.Services.AddControllersWithViews();

// 🔑 Configura autenticação por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";        // rota da tela de login
        options.AccessDeniedPath = "/Login/Index"; // rota se acesso negado
    });

var app = builder.Build();

// Configurações de ambiente
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middlewares padrão
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 🔑 Ativa autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Rota padrão MVC → agora começa pelo Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"
);

// Inicia a aplicação
app.Run();
