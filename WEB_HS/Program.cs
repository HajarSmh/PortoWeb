using Microsoft.EntityFrameworkCore;
using WEB_HS.Data;
using WEB_HS.Services;

var builder = WebApplication.CreateBuilder(args);

// SERVICES 
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<MyDbContextPortfolio>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cnnDBPort")));

// Enregistrement des services métier
builder.Services.AddScoped<ProfilService>();
builder.Services.AddScoped<AuthentificationService>();
builder.Services.AddScoped<ProjetService>();
builder.Services.AddScoped<CompetenceService>();

var app = builder.Build();

// MIDDLEWARES
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Lancement
app.Run();