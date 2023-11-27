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

    }
}
