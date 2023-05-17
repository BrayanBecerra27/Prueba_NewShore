using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_WEB.Controllers;
using NUnit.Framework;

namespace NEWSHORE_AIR_TEST.Tests.Controllers
{
    [TestFixture]
    public class QueryRouteControllerTests
    {
        private QueryRouteController _controller;
        private Mock<ILogger<QueryRouteController>> _loggerMock;
        private Mock<IQueryRoute> _queryRouteMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<QueryRouteController>>();
            _queryRouteMock = new Mock<IQueryRoute>();
            _controller = new QueryRouteController(_loggerMock.Object, _queryRouteMock.Object);
        }

        [Test]
        public async Task Get_ValidRequestWithFlights_ReturnsOkResult()
        {
            // Arrange
            var request = new RouteRequest() { Origin=string.Empty, Destination = string.Empty, RouteType = NEWSHORE_AIR_BUSINESS.Enumerator.RouteType.Unique};
            var expectedResponse = new Journey(string.Empty,string.Empty,0, new List<Flight> { new Flight() }){};
            _queryRouteMock.Setup(x => x.GetRoute(It.IsAny<RouteRequest>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedResponse, okResult.Value);
        }

        [Test]
        public async Task Get_ValidRequestWithoutFlights_ReturnsOkResultWithMessage()
        {
            // Arrange
            var request = new RouteRequest();
            var expectedMessage = "Su consulta no puede ser procesada";
            var expectedResponse = new Journey(string.Empty, string.Empty, 0, new List<Flight> { }){};
            _queryRouteMock.Setup(x => x.GetRoute(It.IsAny<RouteRequest>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okResult.Value);
        }

        [Test]
        public async Task Get_ExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var request = new RouteRequest();
            _queryRouteMock.Setup(x => x.GetRoute(It.IsAny<RouteRequest>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Su consulta no puede ser procesada", badRequestResult.Value);
        }
    }
}
