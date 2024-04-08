using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using System.Security.Claims;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class FormationsControllerTests
    {
        private readonly Mock<IGenericUnitOfWork<Formation>> _unitOfWorkMock;
        private readonly DbContextOptions<DataContext> _options;
        private DataContext _mockDbContext = null!;
        private FormationsController _controller = null!;
        private Mock<IUserHelper> _mockUserHelper = null!;

        public FormationsControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<Formation>>();
            _mockUserHelper = new Mock<IUserHelper>();
        }

        [TestInitialize]
        public void SetUp()
        {
            _mockUserHelper = new Mock<IUserHelper>();
            // Setting up InMemory database
            _mockDbContext = new DataContext(_options);
            _controller = new FormationsController(_unitOfWorkMock.Object, _mockDbContext, _mockUserHelper.Object);

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
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<Formation>(_unitOfWorkMock.Object, context);
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
            var controller = new GenericController<Formation>(_unitOfWorkMock.Object, context);
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
        public async Task GetAsync_FormationFound_ReturnsOk()
        {
            // Arrange
            _mockDbContext.Formations.Add(new Formation
            {
                Id = 1,
                Cod = "c01",
                Status = true,
                Description = "Some"
            }) ;
            await _mockDbContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAsync_FormationNotFound_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
