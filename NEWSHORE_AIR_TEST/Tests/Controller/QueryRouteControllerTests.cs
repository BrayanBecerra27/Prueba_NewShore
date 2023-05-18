using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NEWSHORE_AIR_API.ViewModel;
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
        public async Task Get_ReturnsOkResultWithData_WhenResponseHasFlights()
        {
            // Arrange
            var request = new RouteRequest();
            var response = new JourneyResponse(string.Empty, string.Empty, 0, new List<FlightResponse>() { new FlightResponse()}) {  };
            _queryRouteMock.Setup(x => x.GetRoute(It.IsAny<RouteRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOf<ResponseBase<JourneyResponse>>(okResult.Value);
            var responseBase = okResult.Value as ResponseBase<JourneyResponse>;
            Assert.AreEqual(response, responseBase.Data);
        }

        [Test]
        public async Task Get_ReturnsOkResultWithStringData_WhenResponseHasNoFlights()
        {
            // Arrange
            var request = new RouteRequest();
            var response = new JourneyResponse(string.Empty , string.Empty, 0, new List<FlightResponse>()) { };
            _queryRouteMock.Setup(x => x.GetRoute(It.IsAny<RouteRequest>())).ReturnsAsync(response);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOf<ResponseBase<string>>(okResult.Value);
            var responseBase = okResult.Value as ResponseBase<string>;
            Assert.AreEqual("Su consulta no puede ser procesada", responseBase.Data);
        }

        [Test]
        public async Task Get_ReturnsBadRequestResult_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new RouteRequest();
            _queryRouteMock.Setup(x => x.GetRoute(It.IsAny<RouteRequest>())).Throws(new Exception());

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<ResponseBase<string>>(badRequestResult.Value);
            var responseBase = badRequestResult.Value as ResponseBase<string>;
            Assert.AreEqual("Su consulta no puede ser procesada", responseBase.Data);
        }
    }
}
