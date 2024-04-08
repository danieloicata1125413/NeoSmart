﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class SlidersControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Slider>> _unitOfWorkMock;
        private readonly Mock<IFileStorage> _mockFileStorage = null!;
        public SlidersControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Slider>>();
            _mockFileStorage = new Mock<IFileStorage>();
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new SlidersController(_unitOfWorkMock.Object, context, _mockFileStorage.Object);

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
            var controller = new SlidersController(_unitOfWorkMock.Object, context, _mockFileStorage.Object);
            var pagination = new PaginationDTO { Filter = "Some" };

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
            var controller = new SlidersController(_unitOfWorkMock.Object, context, _mockFileStorage.Object);
            var pagination = new PaginationDTO { Filter = "Some" };

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
        public async Task GetAsync_SliderFound_ReturnsOk()
        {
            // Arrange
            using var _context = new DataContext(_options);
            _context.Sliders.Add(new Slider
            {
                Id = 1, 
                Image = "Some", 
                Description = "Some"
            });
            await _context.SaveChangesAsync();
            var _controller = new SlidersController(_unitOfWorkMock.Object, _context, _mockFileStorage.Object);
            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_SliderNotFound_ReturnsNotFound()
        {
            // Arrange
            using var _context = new DataContext(_options);
            var _controller = new SlidersController(_unitOfWorkMock.Object, _context, _mockFileStorage.Object);
            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
