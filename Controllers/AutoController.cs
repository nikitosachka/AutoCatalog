using Microsoft.AspNetCore.Mvc;
using AutoCatalog.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace AutoCatalog.Controllers
{
    public class AutoController : Controller
    {
        private readonly AutoContext _context;
        private readonly IMemoryCache _cache;

        public AutoController(AutoContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // Головна сторінка з топ-5 популярних авто
        public async Task<IActionResult> Index()
        {
            const string cacheKey = "TopCars";

            if (!_cache.TryGetValue(cacheKey, out List<Auto> topCars))
            {
                topCars = await _context.Cars
                    .OrderByDescending(c => c.ViewCount)
                    .Take(5)
                    .ToListAsync();

                _cache.Set(cacheKey, topCars, TimeSpan.FromMinutes(10));
            }

            return View(topCars);
        }

        // Сторінка каталогу
        public async Task<IActionResult> Catalog()
        {
            var cars = await _context.Cars.ToListAsync();

            foreach (var car in cars)
            {
                car.ImageData = car.Image != null
                    ? Convert.ToBase64String(car.Image)
                    : string.Empty;
            }

            return View(cars);
        }

        // Сторінка деталей авто
        public IActionResult Details(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            car.ViewCount++;
            _context.SaveChanges();
            _cache.Remove("TopCars");

            car.ImageData = car.Image != null
                ? Convert.ToBase64String(car.Image)
                : string.Empty;

            return View(car);
        }

        // Форма покупки (GET)
        [HttpGet]
        public IActionResult Buy(int id)
        {
            var car = _context.Cars.FirstOrDefault(a => a.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var model = new BuyViewModel
            {
                CarId = car.Id,
                CarName = $"{car.Brand} {car.Model}"
            };

            return View(model);
        }

        // Обробка форми покупки (POST)
        [HttpPost]
        public async Task<IActionResult> Buy(BuyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Car = model.CarName
                };

                _context.Clients.Add(client);

                var car = await _context.Cars.FindAsync(model.CarId);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Catalog");
            }

            return View(model);
        }

        // Сторінка "Про нас"
        public IActionResult About()
        {
            return View();
        }
    }
}