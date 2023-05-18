using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NEWSHORE_AIR_API.ViewModel;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_BUSINESS.ViewModel;
using NEWSHORE_AIR_WEB.Controllers;
using NUnit.Framework;
using NEWSHORE_AIR_BUSINESS.Enumerator;

namespace NEWSHORE_AIR_TEST.Tests.Controllers
{
    [TestFixture]
    public class QueryRouteControllerTests
    {
        private Mock<ILogger<QueryRouteController>> _loggerMock;
        private Mock<IQueryRoute> _queryRouteMock;
        private QueryRouteController _controller;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<QueryRouteController>>();
            _queryRouteMock = new Mock<IQueryRoute>();
            _controller = new QueryRouteController(_loggerMock.Object, _queryRouteMock.Object);
        }

        [Test]
        public async Task Get_WithValidRequest_Returns200Ok()
        {
            // Arrange
            var request = new RouteRequest { Origin = string.Empty, Destination = string.Empty,Scale = 0, RouteType = RouteType.Unique   };
            var response = new JourneyResponse { Origin = request.Origin, Destination = request.Destination, Price = 0, Flights = new List<FlightResponse>() { new FlightResponse()} };
            _queryRouteMock.Setup(x => x.GetRoute(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.Get(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<ResponseBase<JourneyResponse>>(okResult.Value);
            var responseBase = okResult.Value as ResponseBase<JourneyResponse>;
            Assert.AreEqual(response, responseBase.Data);
        }

        [Test]
        public async Task Get_WithEmptyResponse_Returns204NoContent()
        {
            // Arrange
            var request = new RouteRequest { Origin = string.Empty, Destination = string.Empty, Scale = 0, RouteType = RouteType.Unique };

            var response = new JourneyResponse { Flights = new List<FlightResponse>() };
            _queryRouteMock.Setup(x => x.GetRoute(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.Get(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task Get_WithException_ReturnsInternalServerError()
        {
            // Arrange
            var request = new RouteRequest { };
            _queryRouteMock.Setup(x => x.GetRoute(request)).Throws<Exception>();

            // Act
            var result = await _controller.Get(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
