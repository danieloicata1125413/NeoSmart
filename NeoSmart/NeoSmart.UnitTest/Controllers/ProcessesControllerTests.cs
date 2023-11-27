using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class ProcessesControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Process>> _unitOfWorkMock;
        public ProcessesControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
     .UseInMemoryDatabase(Guid.NewGuid().ToString())
     .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Process>>();
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult()
        {
            // Arrange
            var context = new DataContext(_options);
            var controller = new ProcessesController(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetComboAsync() as OkObjectResult;

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
            var controller = new ProcessesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO();

            // Act
            var result = await controller.GetAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }
        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new ProcessesController(_unitOfWorkMock.Object, context);
            var pagination = new PaginationDTO();

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFoundWhenEntityNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new ProcessesController(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
