using Microsoft.AspNetCore.Mvc;
using AutoCatalog.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AutoCatalog.Controllers
{
    public class AdminController : Controller
    {
        private readonly AutoContext _context;

        public AdminController(AutoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new Admin());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admin
                    .FirstOrDefaultAsync(a => a.Login == model.Login && a.Password == model.Password);

                if (admin != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, admin.Login)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("EditCars");
                }

                ModelState.AddModelError("", "Неправильний логін або пароль");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditCars()
        {
            var cars = await _context.Cars.ToListAsync();
            var carsWithImages = cars.Select(car =>
            {
                if (car.Image != null)
                {
                    car.ImageData = Convert.ToBase64String(car.Image);
                }
                return car;
            }).ToList();
            return View(carsWithImages);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditCar(int id, string Brand, string Model, int Year, decimal Price, string BodyType, string Color)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                car.Brand = Brand;
                car.Model = Model;
                car.Year = Year;
                car.Price = Price;
                car.BodyType = BodyType;
                car.Color = Color;

                var image = HttpContext.Request.Form.Files["Image"];
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        car.Image = memoryStream.ToArray();
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("EditCars");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCar(Auto car, IFormFile image)
        {
            if (image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    car.Image = memoryStream.ToArray();
                }

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("EditCars");
            }

            return View("EditCars", await _context.Cars.ToListAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("EditCars");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
