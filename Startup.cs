using AutoCatalog.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

// Додаємо аутентифікацію через Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";       // маршрут для сторінки логіну
        options.AccessDeniedPath = "/Admin/Login"; // маршрут при відмові в доступі
    });

builder.Services.AddControllersWithViews();

// Налаштування бази даних
builder.Services.AddDbContext<AutoContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 25))));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Додаємо middleware аутентифікації та авторизації
app.UseAuthentication();
app.UseAuthorization();

// Маршрути
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auto}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
