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
    public class TrainingCalendarsControllerTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<TrainingCalendar>> _unitOfWorkMock;
        private Mock<IFileStorage> _mockFileStorage = null!;

        public TrainingCalendarsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
     .UseInMemoryDatabase(Guid.NewGuid().ToString())
     .Options;
            _unitOfWorkMock = new Mock<IGenericUnitOfWork<TrainingCalendar>>();
            _mockFileStorage = new Mock<IFileStorage>();
        }

        [TestMethod]

        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new TrainingCalendarsController(_unitOfWorkMock.Object, context, _mockFileStorage.Object);
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
  



    }
}
