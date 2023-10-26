using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Intertfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class CitiesControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<City>> _unitOfWorkMock;

        public CitiesControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<City>>();
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CitiesController(_unitOfWorkMock.Object, context);
            var stateId = 1;

            // Act
            var result = await controller.GetComboAsync(stateId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CitiesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO { Id = 1, Filter = "Some" };

            // Act
            var result = await controller.GetAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new CitiesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO { Id = 1, Filter = "Some" };

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }
    }
}