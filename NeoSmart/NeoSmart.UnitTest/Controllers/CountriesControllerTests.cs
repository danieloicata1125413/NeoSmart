using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.Backend.Controllers;
using NeoSmart.Backend.Interfaces;
using NeoSmart.Backend.Intertfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using System.Security.Claims;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class CountriesControllerTests
    {
        private readonly Mock<IGenericUnitOfWork<Country>> _unitOfWorkMock;
        private readonly DbContextOptions<DataContext> _options;
        private DataContext _mockDbContext = null!;
        private CountriesController _controller = null!;
        private Mock<IUserHelper> _mockUserHelper = null!;

        public CountriesControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Country>>();
        }

        [TestInitialize]
        public void SetUp()
        {
            _mockUserHelper = new Mock<IUserHelper>();
            // Setting up InMemory database
            _mockDbContext = new DataContext(_options);
            _controller = new CountriesController(_unitOfWorkMock.Object, _mockDbContext);

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

            // Act
            var result = await _controller.GetComboAsync() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange

            var pagination = new PaginationDTO { Filter = "some" };

            // Act
            var result = await _controller.GetAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkResult()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "some" };

            // Act
            var result = await _controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFoundWhenCountryNotFound()
        {
            // Arrange

            // Act
            var result = await _controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkWhenCountryFound()
        {
            // Arrange
            using var context = new DataContext(new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            var country = new Country { Id = 1, Name = "test", Cod = 1, Status = true  };
            _unitOfWorkMock.Setup(x => x.GetCountryAsync(country.Id)).ReturnsAsync(country);
            var controller = new CountriesController(_unitOfWorkMock.Object, context);
            // Act
            var result = await controller.GetAsync(country.Id) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _unitOfWorkMock.Verify(x => x.GetCountryAsync(country.Id), Times.Once());

        }
    }
}