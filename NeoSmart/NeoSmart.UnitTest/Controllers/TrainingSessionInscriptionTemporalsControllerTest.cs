using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NeoSmart.BackEnd.Controllers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using NeoSmart.UnitTest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NeoSmart.UnitTest.Controllers
{
    [TestClass]
    public class SessionInscriptionTemporalsControllerTest
    {
        private SessionInscriptionTemporalsController _controller = null!;
        private DataContext _context = null!;
        private DbContextOptions<DataContext> _options = null!;
        private Mock<IGenericUnitOfWork<SessionInscriptionTemporal>> _mockUnitOfWork = null!;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new DataContext(_options);
            _mockUnitOfWork = new Mock<IGenericUnitOfWork<SessionInscriptionTemporal>>();
            _controller = new SessionInscriptionTemporalsController(_mockUnitOfWork.Object, _context);

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
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnTemporalInscription_WhenIdExists()
        {
            // Arrange
            var expectedInscription = new SessionInscriptionTemporal { Id = 1 };
            _context.SessionInscriptionTemporals.Add(expectedInscription);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PutAsync_ShouldUpdateTemporalInscription_WhenOrderExists()
        {
            // Arrange
            var expectedInscription = new SessionInscriptionTemporal { Id = 1, Remarks = "Initial Remarks"};
            _context.SessionInscriptionTemporals.Add(expectedInscription);
            await _context.SaveChangesAsync();

            var dto = new SessionInscriptionTemporalDTO { Id = 1, Remarks = "Updated Remarks"};

            // Act
            var result = await _controller.PutAsync(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var returnedDto = okResult!.Value as SessionInscriptionTemporalDTO;
            Assert.AreEqual(dto.Remarks, returnedDto!.Remarks);
        }

        [TestMethod]
        public async Task PutAsync_ShouldReturnNotFound_WhenTemporalInscriptionDoesNotExistAsync()
        {
            // Arrange
            var dto = new SessionInscriptionTemporalDTO { Id = 1, Remarks = "Some Remarks" };

            // Act
            var result = await _controller.PutAsync(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostAsync_ShouldAddTemporalInscription_WhenDataIsValid()
        {
            // Arrange
            var session = new Session { Id = 1, 
                DateStart = DateTime.Now,
                Training = new Training { Id = 1,
                    Description = "Test",
                    Duration = 60,
                    Type = true,
                    Status = true,
                },
                Type = false,
                Status = true
            };
            var user = new User { Email = "test@example.com", Address = "Some", Document = "Some", FirstName = "John", LastName = "Doe" };
            _context.Sessions.Add(session);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var dto = new SessionInscriptionTemporalDTO { SessionId = 1, Remarks = "New Remarks"};

            // Act
            var result = await _controller.PostAsync(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PostAsync_ShouldReturnNotFound_WhenSessionDoesNotExistAsync()
        {
            // Arrange
            var dto = new SessionInscriptionTemporalDTO { SessionId = 1 };

            // Act
            var result = await _controller.PostAsync(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostAsync_ShouldReturnNotFound_WhenUserDoesNotExistAsync()
        {
            // Arrange
            var session = new Session
            {
                Id = 1,
                DateStart = DateTime.Now,
                Training = new Training
                {
                    Id = 1,
                    Description = "Test",
                    Duration = 60,
                    Type = true,
                    Status = true,
                },
                Type = false,
                Status = true
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            var dto = new SessionInscriptionTemporalDTO { SessionId = 1 };

            // Act
            var result = await _controller.PostAsync(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostAsync_ShouldAddTemporalInscription_WhenSessionAndUserExistAsync()
        {
            // Arrange
            var session = new Session
            {
                Id = 1,
                DateStart = DateTime.Now,
                Training = new Training
                {
                    Id = 1,
                    Description = "Test",
                    Duration = 60,
                    Type = true,
                    Status = true,
                },
                Type = false,
                Status = true
            };
            _context.Sessions.Add(session);

            var user = new User { Email = "test@example.com", Address = "Some", Document = "Some", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            var dto = new SessionInscriptionTemporalDTO { SessionId = 1, Remarks = "Test Remarks" };

            // Act
            var result = await _controller.PostAsync(dto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var returnedDto = okResult!.Value as SessionInscriptionDTO;
            Assert.AreEqual(dto.Remarks, returnedDto!.Remarks);
        }

        [TestMethod]
        public async Task PostAsync_ExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var exceptionalContext = new ExceptionalDataContext(options);
            var email = "test@example.com";

            exceptionalContext.Sessions.Add(
                new Session
                {
                    Id = 1,
                    DateStart = DateTime.Now,
                    Training = new Training
                    {
                        Id = 1,
                        Description = "Test",
                        Duration = 60,
                        Type = true,
                        Status = true,
                    },
                    Type = false,
                    Status = true
                });
            exceptionalContext.Users.Add(new User { Email = email, Address = "Some", Document = "Some", FirstName = "John", LastName = "Doe" });
            exceptionalContext.SaveChanges();

            var controller = CreateControllerWithMockedUserEmail(email, exceptionalContext);

            var SessionInscriptionTemporalDTO = new SessionInscriptionTemporalDTO {  SessionId = 1, Remarks = "TestRemarks" };

            // Act
            var result = await controller.PostAsync(SessionInscriptionTemporalDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual("Test Exception", ((BadRequestObjectResult)result).Value);
        }

        private SessionInscriptionTemporalsController CreateControllerWithMockedUserEmail(string email, DataContext context)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var controller = new SessionInscriptionTemporalsController(null, context);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            return controller;
        }

        [TestMethod]
        public async Task GetAsync_WithUserHavingData_ReturnsCorrectData()
        {
            // Arrange
            var session = new Session
            {
                Id = 1,
                DateStart = DateTime.Now,
                Training = new Training
                {
                    Id = 1,
                    Description = "Test",
                    Duration = 60,
                    Type = true,
                    Status = true,
                },
                Type = false,
                Status = true
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            var user = new User { Email = "test@example.com", Address = "Some", Document = "Some", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var temporalInscription = new SessionInscriptionTemporal { Session = session, Remarks = "Some", User = user };
            _context.SessionInscriptionTemporals.Add(temporalInscription);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var data = okResult!.Value as List<SessionInscriptionTemporal>;
            Assert.AreEqual(1, data!.Count);
        }

        [TestMethod]
        public async Task GetCountAsync_WithUserHavingData_ReturnsCorrectCount()
        {
            // Arrange
            var session = new Session
            {
                Id = 1,
                DateStart = DateTime.Now,
                Training = new Training
                {
                    Id = 1,
                    Description = "Test",
                    Duration = 60,
                    Type = true,
                    Status = true,
                },
                Type = false,
                Status = true
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            var user = new User { Email = "test@example.com", Address = "Some", Document = "Some", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.SessionInscriptionTemporals.Add(new SessionInscriptionTemporal { Session = session, Remarks = "Some", User = user });
            _context.SessionInscriptionTemporals.Add(new SessionInscriptionTemporal { Session = session, Remarks = "Any", User = user });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetCountAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var count = (int)okResult!.Value!;
            Assert.AreEqual(2, count);
        }
    }
}
