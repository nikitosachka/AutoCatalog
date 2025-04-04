using Microsoft.AspNetCore.Mvc;
using AutoCatalog.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoCatalog.Controllers
{
    public class AutoController : Controller
    {
        private readonly AutoContext _context;

        public AutoController(AutoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Catalog()
        {
            var autos = await _context.Cars.ToListAsync();

            var autosWithImages = autos.Select(auto => 
            {
                auto.ImageData = Convert.ToBase64String(auto.Image);
                return auto;
            }).ToList();

            return View(autosWithImages);
        }

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
                await _context.SaveChangesAsync();

                var car = await _context.Cars.FindAsync(model.CarId);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Catalog");
            }

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
