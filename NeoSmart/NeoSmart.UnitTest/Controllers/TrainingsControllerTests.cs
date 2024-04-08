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
    public class TrainingsControllerTests
    {
        private TrainingsController _controller = null!;
        private DataContext _context = null!;
        private Mock<IFileStorage> _mockFileStorage = null!;
        private Mock<IGenericUnitOfWork<Training>> _mockUnitOfWork = null!;
        private Mock<IUserHelper> _mockUserHelper = null!;
        private const string _container = "trainings";
        private const string _string64base = "U29tZVZhbGlkQmFzZTY0U3RyaW5n";

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _mockFileStorage = new Mock<IFileStorage>();
            _mockUnitOfWork = new Mock<IGenericUnitOfWork<Training>>();
            _mockUserHelper = new Mock<IUserHelper>();
            _controller = new TrainingsController(_mockUnitOfWork.Object, _context, _mockFileStorage.Object, _mockUserHelper.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using var context = new DataContext(options);
            var controller = new TrainingsController(_mockUnitOfWork.Object, context, _mockFileStorage.Object, _mockUserHelper.Object);
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
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using var context = new DataContext(options);
            var controller = new TrainingsController(_mockUnitOfWork.Object, context, _mockFileStorage.Object, _mockUserHelper.Object);
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
        public async Task GetAsync_TrainingFound_ReturnsOk()
        {
            // Arrange
            _context.Trainings.Add(new Training
            {
                Id = 1,
                Process = new Process
                {
                    Id = 1,
                    Cod = "P01",
                    Status = true,
                    Description = "Some"
                },
                ProcessId = 1,
                Cod = "T01", 
                Duration = 60,
                Status = true,
                Description = "Some"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_TrainingNotFound_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostAddImagesAsync_TrainingNotFound_ReturnsNotFound()
        {
            // Arrange
            var imageDto = new ImageDTO {  Id = 999 };

            // Act
            var result = await _controller.PostAddImagesAsync(imageDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostAddImagesAsync_SuccessfullyAddsImages_ReturnsOk()
        {
            // Arrange
            _context.Trainings.Add(new Training
            {
                Id = 1,
                Process = new Process
                {
                    Id = 1,
                    Cod = "P01",
                    Status = true,
                    Description = "Some"
                },
                ProcessId = 1,
                Cod = "T01",
                Duration = 60,
                Status = true,
                Description = "Some"
            });
            await _context.SaveChangesAsync();

            var imageDto = new ImageDTO
            {
                Id = 1,
                Images = new List<string> { _string64base }
            };

            _mockFileStorage.Setup(fs => fs.SaveFileAsync(It.IsAny<byte[]>(), ".jpg", _container)).ReturnsAsync("storedImagePath");

            // Act
            var result = await _controller.PostAddImagesAsync(imageDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as ImageDTO;
            Assert.IsTrue(returnedDto!.Images.Contains("storedImagePath"));
            _mockFileStorage.Verify(x => x.SaveFileAsync(It.IsAny<byte[]>(), ".jpg", _container), Times.Once());
        }

        [TestMethod]
        public async Task PostRemoveLastImageAsync_TrainingNotFound_ReturnsNotFound()
        {
            // Arrange
            var imageDto = new ImageDTO { Id = 999 }; // Non-existing product

            // Act
            var result = await _controller.PostRemoveLastImageAsync(imageDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostRemoveLastImageAsync_NoImages_ReturnsOkWithEmptyList()
        {
            // Arrange
            _context.Trainings.Add(new Training
            {
                Id = 1,
                Process = new Process
                {
                    Id = 1,
                    Cod = "P01",
                    Status = true,
                    Description = "Some"
                },
                ProcessId = 1,
                Cod = "T01",
                Duration = 60,
                Status = true,
                Description = "Some"
            });
            await _context.SaveChangesAsync();

            var imageDto = new ImageDTO { Id = 1 };

            // Act
            var result = await _controller.PostRemoveLastImageAsync(imageDto);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task PostRemoveLastImageAsync_RemovesLastImage_ReturnsOk()
        {
            // Arrange
            _context.Trainings.Add(new Training
            {
                Id = 1,
                Process = new Process
                {
                    Id = 1,
                    Cod = "P01",
                    Status = true,
                    Description = "Some"
                },
                ProcessId = 1,
                TrainingImages = new List<TrainingImage>
                {
                    new TrainingImage { Image = "image1.jpg" },
                    new TrainingImage { Image = "image2.jpg" }
                },
                Cod = "T01",
                Duration = 60,
                Status = true,
                Description = "Some"
            });
            await _context.SaveChangesAsync();

            _mockFileStorage.Setup(fs => fs.RemoveFileAsync("image2.jpg", "trainings"))
                .Returns(Task.CompletedTask);

            var imageDto = new ImageDTO { Id = 1 };

            // Act
            var result = await _controller.PostRemoveLastImageAsync(imageDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as ImageDTO;
            Assert.AreEqual(1, returnedDto!.Images.Count);
            Assert.AreEqual("image1.jpg", returnedDto.Images.First());
            _mockFileStorage.Verify(x => x.RemoveFileAsync("image2.jpg", "trainings"), Times.Once());
        }
    }
}
