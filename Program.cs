using Microsoft.AspNetCore.Authentication.Cookies;
using Kanban.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://webfamilyhomeapi-production.up.railway.app/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
