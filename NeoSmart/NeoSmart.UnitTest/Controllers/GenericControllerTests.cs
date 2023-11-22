using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class GenericControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<DocumentType>> _unitOfWorkMock;

        public GenericControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<DocumentType>>();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);
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
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);
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
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkWhenEntityFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "Some" };
            _unitOfWorkMock.Setup(x => x.GetAsync(documentType.Id)).ReturnsAsync(documentType);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.GetAsync(documentType.Id) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.GetAsync(documentType.Id), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task PostAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "Some" };
            var response = new Response<DocumentType> { IsSuccess = true };
            _unitOfWorkMock.Setup(x => x.AddAsync(documentType)).ReturnsAsync(response);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PostAsync(documentType) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.AddAsync(documentType), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task PostAsync_ReturnsBadRequest()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "Some" };
            var response = new Response<DocumentType> { IsSuccess = false };
            _unitOfWorkMock.Setup(x => x.AddAsync(documentType)).ReturnsAsync(response);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PostAsync(documentType) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.AddAsync(documentType), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task PutAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "Some" };
            var response = new Response<DocumentType> { IsSuccess = true };
            _unitOfWorkMock.Setup(x => x.UpdateAsync(documentType)).ReturnsAsync(response);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PutAsync(documentType) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.UpdateAsync(documentType), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task PutAsync_ReturnsBadRequest()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "Some" };
            var response = new Response<DocumentType> { IsSuccess = false };
            _unitOfWorkMock.Setup(x => x.UpdateAsync(documentType)).ReturnsAsync(response);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.PutAsync(documentType) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.UpdateAsync(documentType), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsNoContentWhenEntityDeleted()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "test" };
            _unitOfWorkMock.Setup(x => x.GetAsync(documentType.Id)).ReturnsAsync(documentType);
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.DeleteAsync(documentType.Id) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.GetAsync(documentType.Id), Times.Once());

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task DeleteAsync_ReturnsNoContentWhenEntityNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var documentType = new DocumentType { Id = 1, Name = "test" };
            var controller = new GenericController<DocumentType>(_unitOfWorkMock.Object, context);

            // Act
            var result = await controller.DeleteAsync(documentType.Id) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}