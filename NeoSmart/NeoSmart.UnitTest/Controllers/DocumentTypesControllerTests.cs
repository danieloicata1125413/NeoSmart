using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.Data.Entities;
using System.Security.Claims;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class DocumentTypesControllerTests
    {
        private DataContext _mockDbContext = null!;
        private DocumentTypesController _controller = null!;
        private Mock<IUserHelper> _mockUserHelper = null!;

        [TestInitialize]
        public void SetUp()
        {
            _mockUserHelper = new Mock<IUserHelper>();
            // Setting up InMemory database
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            _mockDbContext = new DataContext(options);
            _controller = new DocumentTypesController(_mockDbContext);

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

    }
}
