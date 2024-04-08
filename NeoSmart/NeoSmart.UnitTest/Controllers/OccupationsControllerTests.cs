using Microsoft.AspNetCore.Http;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class OccupationsControllerTests
    {

        private readonly Mock<IGenericUnitOfWork<Occupation>> _unitOfWorkMock;
        private readonly DbContextOptions<DataContext> _options;
        private DataContext _mockDbContext = null!;
        private OccupationsController _controller = null!;
        private Mock<IUserHelper> _mockUserHelper = null!;

        public OccupationsControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Occupation>>();
        }

        [TestInitialize]
        public void SetUp()
        {
            _mockUserHelper = new Mock<IUserHelper>();
            // Setting up InMemory database
            _mockDbContext = new DataContext(_options);
            _controller = new OccupationsController(_unitOfWorkMock.Object, _mockDbContext, _mockUserHelper.Object);

            // Mock user identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test@example.com")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clean up (if needed)
            _mockDbContext.Database.EnsureDeleted();
            _mockDbContext.Dispose();
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new OccupationsController(_unitOfWorkMock.Object, context,_mockUserHelper.Object);

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
            var controller = new GenericController<Occupation>(_unitOfWorkMock.Object, context);
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
            var controller = new GenericController<Occupation>(_unitOfWorkMock.Object, context);
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
        public async Task GetAsync_OccupationFound_ReturnsOk()
        {
            // Arrange
            _mockDbContext.Occupations.Add(new Occupation
            {
                Id = 1,
                Cod = "O01",
                Status = true,
                Process = new Process
                {
                    Id = 1,
                    Cod = "P01",
                    Status = true,
                    Description = "Some"
                },
                ProcessId = 1,
                Description = "Some"
            });
            await _mockDbContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_OccupationNotFound_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
