using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class InscriptionsControllerTests
    {

        private Mock<IInscriptionsHelper> _mockInscriptionsHelper = null!;
        private readonly DbContextOptions<DataContext> _options;
        private Mock<IUserHelper> _mockUserHelper = null!;
        private Mock<IMailHelper> _mockMailHelper = null!;
        private InscriptionsController _controller = null!;

        public InscriptionsControllerTests()
        {
            _mockInscriptionsHelper = new Mock<IInscriptionsHelper>();
            _mockUserHelper = new Mock<IUserHelper>();
            _mockMailHelper = new Mock<IMailHelper>();

            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            //Arrange
            using var context = new DataContext(_options);
            var Id = 1;

            var controller = new InscriptionsController(
                Mock.Of<IInscriptionsHelper>(),
                context,
                Mock.Of<IUserHelper>(),
                Mock.Of<IMailHelper>()
            );

            //Act
            var result = await controller.GetAsync(Id) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFoundWhenInscriptionNotFound()
        {
            //Arrange
            using var context = new DataContext(_options);
            var Id = 67;

            var controller = new InscriptionsController(
                Mock.Of<IInscriptionsHelper>(),
                context,
                Mock.Of<IUserHelper>(),
                Mock.Of<IMailHelper>()
            );

            //Act
            var result = await controller.GetAsync(Id) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_PaginationDTO_BadRequestObjectResult()
        {
            //Arrange
            var context = new DataContext(_options);
            var pagination = new PaginationDTO { Id = 1, Filter = "Excel" };
            var userName = "testuser";
            var mockUser = new User();

            var controller = new InscriptionsController(
                Mock.Of<IInscriptionsHelper>(),
                context,
                Mock.Of<IUserHelper>(),
                Mock.Of<IMailHelper>()
            );

            controller.ControllerContext = GetControllerContext(userName);
            _mockUserHelper.Setup(x => x.GetUserAsync(userName))
                .ReturnsAsync(mockUser);

            //Act
            var result = await controller.GetAsync(pagination) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);

            // Clean up (if needed)
            context.Database.EnsureDeleted();
        }

        private ControllerContext GetControllerContext(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };
            var identity = new ClaimsIdentity(claims, "test");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };
            return new ControllerContext
            {
                HttpContext = httpContext
            };
        }

    }
}
