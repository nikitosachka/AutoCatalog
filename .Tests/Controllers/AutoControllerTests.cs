using Xunit;
using AutoCatalog.Controllers;
using AutoCatalog.Data;
using AutoCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCatalog.Tests.Controllers
{
    public class AutoControllerTests
    {
        private ApplicationDbContext GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestViewCountDb")
                .Options;

            var context = new ApplicationDbContext(options);

            if (!context.Autos.Any())
            {
                context.Autos.AddRange(
                    new Auto { Id = 1, Brand = "Tesla", Model = "Model S", Year = 2021, ViewCount = 0 },
                    new Auto { Id = 2, Brand = "Ford", Model = "Mustang", Year = 2020, ViewCount = 0 }
                );
                context.SaveChanges();
            }

            return context;
        }

        [Fact]
        public async Task ViewCount_Increments_WhenCarIsViewed()
        {
            // Arrange
            var context = GetDbContextWithData();
            var controller = new AutoController(context);

            // Act: Переглядаємо авто з ID = 1
            await controller.Details(1);

            // Перевіряємо, що лічильник переглядів збільшився
            var car = context.Autos.FirstOrDefault(a => a.Id == 1);
            Assert.NotNull(car);
            Assert.Equal(2, car.ViewCount); // Лічильник має бути 1 після перегляду
        }
    }
}
