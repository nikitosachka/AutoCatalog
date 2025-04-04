using Xunit;
using AutoCatalog.Controllers;
using AutoCatalog.Data;
using AutoCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCatalog.Tests.Controllers
{
    public class AutoControllerTests
    {
        private ApplicationDbContext GetDbContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAutoDb")
                .Options;

            var context = new ApplicationDbContext(options);

            if (!context.Autos.Any())
            {
                context.Autos.AddRange(
                    new Auto { Id = 1, Brand = "Tesla", Model = "Model S", Year = 2021 },
                    new Auto { Id = 2, Brand = "Ford", Model = "Mustang", Year = 2020 }
                );
                context.SaveChanges();
            }

            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewWithListOfAutos()
        {
            // Arrange
            var context = GetDbContextWithData();
            var controller = new AutoController(context);

            // Act
            var result = await controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Auto>>(viewResult.Model);

            // Assert
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }
    }
}
