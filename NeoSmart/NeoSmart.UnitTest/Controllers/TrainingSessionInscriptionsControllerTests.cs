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

using NeoSmart.ClassLibraries.Responses;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Routing;
using NeoSmart.ClassLibraries.Interfaces;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class TrainingSessionInscriptionsControllerTests
    {

        private Mock<IInscriptionsHelper> _mockInscriptionsHelper = null!;
        private readonly DbContextOptions<DataContext> _options;
        private Mock<IUserHelper> _mockUserHelper = null!;
        private Mock<IMailHelper> _mockMailHelper = null!;
        private TrainingSessionInscriptionsController _controller = null!;
        private DataContext _context = null!;
        private DataContext _mockDbContext = null!;

        public TrainingSessionInscriptionsControllerTests()
        {
            _mockInscriptionsHelper = new Mock<IInscriptionsHelper>();
            _mockUserHelper = new Mock<IUserHelper>();
            _mockMailHelper = new Mock<IMailHelper>();

            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            _mockDbContext = new DataContext(_options);

            _controller = new TrainingSessionInscriptionsController(_mockInscriptionsHelper.Object, _mockDbContext, _mockUserHelper.Object, _mockMailHelper.Object);

        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            //Arrange
            var context = new DataContext(_options);
            var Id = 1;

            var userName = "test@example.com";
            var user = new User { Document = "3435354", UserName = userName, CityId = 1 };
             _mockUserHelper.Setup(x => x.GetUserAsync(userName))
                  .ReturnsAsync(user);
              _mockUserHelper.Setup(x => x.IsUserInRoleAsync(user, UserType.Admin.ToString()))
                  .ReturnsAsync(true);

             
            _mockDbContext.Countries.Add(new Country
            {
                Name = "Colombia",
                Id = 1,
                States = new List<State>
                  {
                      new State
                      {
                          Name = "Antioquia",
                          Id = 1,
                          Cities = new List<City>
                          {
                              new City { Id = 1, Name = "Medellín" }
                          }
                      }
                  }
            });


            _mockDbContext.Users.Add(new User { Id = "ccefa08a-ec19-4035-8543-3b5cc844d8b1", Document = "3435354", FirstName = "Henry", LastName = "Muñoz", DocumentTypeId = 1, Address = "Boston", Photo ="", UserName = userName, CityId = 1 }); ;

            _mockDbContext.Trainings.Add(new Training { Id = 1, Description="", Duration=1, Type=false });

            _mockDbContext.TrainingImages.Add(new TrainingImage { Id = 1, TrainingId = 1, Image =""});

            _mockDbContext.Topics.Add(new Topic { Id = 1,Description="prueba" });

            _mockDbContext.TrainingTopics.Add(new TrainingTopic {Id=1, TrainingId = 1, TopicId = 1 });

            var dat1 = new DateTime();
            TimeSpan interval = new TimeSpan();
            _mockDbContext.Sessions.Add(new Session { Id = 1, DateStart = dat1, DateEnd = dat1, TimeStart= interval, TimeEnd= interval, TrainingId = 1, UserId= "ccefa08a-ec19-4035-8543-3b5cc844d8b1" });

            var datime1 = new DateTime();
            _mockDbContext.TrainingSessionInscriptions.Add(new TrainingSessionInscription { Id = 1, Date= datime1, UserId= "ccefa08a-ec19-4035-8543-3b5cc844d8b1", SessionId = 1, Remarks="Hola mundo" });

            _mockDbContext.SaveChanges();


            //Act
            var result = await _controller.GetAsync(Id) as OkObjectResult;

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

            var controller = new TrainingSessionInscriptionsController(
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

            var controller = new TrainingSessionInscriptionsController(
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

        [TestMethod]
        public async Task GetPagesAsync_()
        {
            //Arrange
            var context = new DataContext(_options);
            var pagination = new PaginationDTO { Id = 1, Filter = "Excel" };
            var userName = "testuser";
            var mockUser = new User();

            var controller = new TrainingSessionInscriptionsController(
                Mock.Of<IInscriptionsHelper>(),
                context,
                Mock.Of<IUserHelper>(),
                Mock.Of<IMailHelper>()
            );

            //////////////////
            var user = new User { Email = "test@example.com" };
            _mockUserHelper.Setup(x => x.GetUserAsync(user.Email))
                .ReturnsAsync(user);
            _mockUserHelper.Setup(x => x.GeneratePasswordResetTokenAsync(user))
                .ReturnsAsync("GeneratedToken");

            var response = new Response<string> { IsSuccess = true };
            _mockMailHelper.Setup(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(response);

            ///////////////

            //Act
            //var result = await controller.GetPagesAsync(pagination) as BadRequestObjectResult;
            var result = await controller.GetPagesAsync(pagination) as BadRequestObjectResult;

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
