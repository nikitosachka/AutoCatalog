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
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // !!! Унікальна база для кожного тесту
                .Options;

            var context = new ApplicationDbContext(options);

            context.Autos.AddRange(
                new Auto { Id = 1, Brand = "Tesla", Model = "Model S", Year = 2021, ViewCount = 0 },
                new Auto { Id = 2, Brand = "Ford", Model = "Mustang", Year = 2020, ViewCount = 0 }
            );

            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task ViewCount_Increments_WhenCarIsViewed()
        {
            // Arrange
            var context = GetDbContextWithData();
            var controller = new AutoController(context);

            // Act
            await controller.Details(1);
            await context.SaveChangesAsync();

            var car = await context.Autos.FindAsync(1);

            // Assert
            Assert.Equal(1, car.ViewCount);
        }
    }
}
