using AutoMapper;
using Moq;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_BUSINESS.Enumerator;
using NEWSHORE_AIR_BUSINESS.Mapper;
using NEWSHORE_AIR_DATAACCESS.Implementation;
using NEWSHORE_AIR_BUSINESS.Interface;
using NUnit.Framework;
using Microsoft.OpenApi.Any;

namespace NEWSHORE_AIR_TEST.Tests.DataAccess
{
    [TestFixture]
    public class QueryRouteTests
    {
        private IMapper _mapper;
        private Mock<IFlightService> _mockFlightService;
        private Mock<IJourneyRepository> _mockJourneyRepository;
        private Mock<ICalculateRouteInteractor> _mockCalculateRouteInteractor;
        private QueryRoute _queryRoute;
        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingFlight>();
            });
            _mapper = configuration.CreateMapper();
            _mockFlightService = new Mock<IFlightService>();
            _mockJourneyRepository = new Mock<IJourneyRepository>();
            _mockCalculateRouteInteractor = new Mock<ICalculateRouteInteractor>();
            _queryRoute = new QueryRoute(_mapper, _mockFlightService.Object, _mockJourneyRepository.Object, _mockCalculateRouteInteractor.Object);
        }

        [Test]
        public async Task GetRoute_ReturnsJourneyWithFlights_WhenRouteResponseNotEmpty()
        {
            // Arrange
            var request = new RouteRequest
            {
                Origin = "A",
                Destination = "F",
                Scale = 2,
                RouteType = RouteType.Unique
            };

            var routeResponse = new List<RouteResponse>
            {
                new RouteResponse { DepartureStation = "A", ArrivalStation = "B" },
                new RouteResponse { DepartureStation = "B", ArrivalStation = "C" },
                new RouteResponse { DepartureStation = "C", ArrivalStation = "F" }
            };
            var flights = new List<Flight>() { new Flight()};
            var journeyDb = new Journey { Flights = flights};
            
            _mockJourneyRepository.Setup(x => x.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString())).ReturnsAsync(journeyDb);
            _mockFlightService.Setup(x => x.GetInformationRoutesAsync(request)).ReturnsAsync(routeResponse);
            _mockCalculateRouteInteractor.Setup(x => x.CalculateRouteAsync(request, routeResponse)).ReturnsAsync(flights);
            // Act
            var journey = await _queryRoute.GetRoute(request);

            // Assert
            Assert.NotNull(journey);
            Assert.AreEqual(1, journey.Flights.Count);
            Assert.AreEqual(0, journey.Price);
        }

        [Test]
        public async Task GetRoute_ReturnsJourneyWithEmptyFlights_WhenRouteResponseEmpty()
        {
            // Arrange
            var request = new RouteRequest
            {
                Origin = "A",
                Destination = "F",
                Scale = 2,
                RouteType = RouteType.Unique
            };

            var routeResponse = new List<RouteResponse>();

            var flights = new List<Flight>() { };
            var journeyDb = new Journey {Origin = request.Origin, Destination = request.Destination, Price = 0, Flights = new List<Flight>() };
            _mockJourneyRepository.Setup(x => x.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString())).ReturnsAsync(journeyDb);
            _mockFlightService.Setup(x => x.GetInformationRoutesAsync(request)).ReturnsAsync(routeResponse);
            _mockCalculateRouteInteractor.Setup(x => x.CalculateRouteAsync(request, routeResponse)).ReturnsAsync(flights);
            // Act
            var journey = await _queryRoute.GetRoute(request);

            // Assert
            Assert.NotNull(journey);
            Assert.IsEmpty(journey.Flights);
            Assert.AreEqual(0, journey.Price);
        }

        [Test]
        public async Task GetRoute_ReturnsJourneyWithFlights_WhenRouteTypeMultipleAndReturn()
        {
            // Arrange
            var request = new RouteRequest
            {
                Origin = "A",
                Destination = "F",
                Scale = 4,
                RouteType = RouteType.MultipleAndReturn
            };

            var routeResponse = new List<RouteResponse>
            {
                new RouteResponse { DepartureStation = "A", ArrivalStation = "B" },
                new RouteResponse { DepartureStation = "B", ArrivalStation = "C" },
                new RouteResponse { DepartureStation = "C", ArrivalStation = "F" },
                new RouteResponse { DepartureStation = "D", ArrivalStation = "A" },
                new RouteResponse { DepartureStation = "C", ArrivalStation = "D" },
                new RouteResponse { DepartureStation = "D", ArrivalStation = "E" },
                new RouteResponse { DepartureStation = "E", ArrivalStation = "F" }
            };

            var flights = new List<Flight>() {  
                new Flight { Destination = "A", Origin = "B" },
                new Flight { Destination = "B", Origin = "C" },
                new Flight { Destination = "C", Origin = "F" }
            
            };

            var journeyDb = new Journey { Origin = request.Origin, Destination = request.Destination, Price = 0, Flights = flights };
            _mockJourneyRepository.Setup(x => x.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString())).ReturnsAsync(journeyDb);

            _mockFlightService.Setup(x => x.GetInformationRoutesAsync(request)).ReturnsAsync(routeResponse);

            // Act
            var journey = await _queryRoute.GetRoute(request);

            // Assert
            Assert.NotNull(journey);
            Assert.AreEqual(3, journey.Flights.Count);
            Assert.AreEqual(0, journey.Price);
        }
        [Test]
        public async Task GetRoute_ReturnsJourneyWithFlights_WhenRouteTypeMultipleAndScaleIsZero()
        {
            // Arrange
            var request = new RouteRequest
            {
                Origin = "A",
                Destination = "F",
                Scale = 0,
                RouteType = RouteType.Multiple
            };

            var routeResponse = new List<RouteResponse>
            {
                new RouteResponse { DepartureStation = "A", ArrivalStation = "B" },
                new RouteResponse { DepartureStation = "B", ArrivalStation = "C" },
                new RouteResponse { DepartureStation = "C", ArrivalStation = "F" }
            };

            var flights = new List<Flight>
            {
                new Flight { Destination = "A", Origin = "B" },
                new Flight { Destination = "B", Origin = "C" },
                new Flight { Destination = "C", Origin = "F" }
            };
            var journeyDb = new Journey { Origin = request.Origin, Destination = request.Destination, Price = 0, Flights = flights };
            _mockJourneyRepository.Setup(x => x.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString())).ReturnsAsync(journeyDb);

            _mockFlightService.Setup(x => x.GetInformationRoutesAsync(request)).ReturnsAsync(routeResponse);

            _mockCalculateRouteInteractor.Setup(x => x.CalculateRouteAsync(request, routeResponse)).ReturnsAsync(flights);
            // Act
            var journey = await _queryRoute.GetRoute(request);

            // Assert
            Assert.NotNull(journey);
            Assert.AreEqual(3, journey.Flights.Count);
            Assert.AreEqual(0, journey.Price);
        }

        [Test]
        public async Task GetRoute_ReturnsJourneyWithEmptyFlights_WhenScaleIsZeroAndNoRouteFound()
        {
            // Arrange
            var request = new RouteRequest
            {
                Origin = "A",
                Destination = "F",
                Scale = 0,
                RouteType = RouteType.Multiple
            };

            var routeResponse = new List<RouteResponse>
            {
                new RouteResponse { DepartureStation = "A", ArrivalStation = "B" },
                new RouteResponse { DepartureStation = "C", ArrivalStation = "D" }
            };

            var flights = new List<Flight>();

            var journeyDb = new Journey { Origin = request.Origin, Destination  = request.Destination, Price = 0, Flights = new List<Flight>() };
            _mockJourneyRepository.Setup(x => x.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString())).ReturnsAsync(journeyDb);

            _mockFlightService.Setup(x => x.GetInformationRoutesAsync(request)).ReturnsAsync(routeResponse);

            _mockCalculateRouteInteractor.Setup(x => x.CalculateRouteAsync(request, routeResponse)).ReturnsAsync(flights);

            // Act
            var journey = await _queryRoute.GetRoute(request);

            // Assert
            Assert.NotNull(journey);
            Assert.IsEmpty(journey.Flights);
            Assert.AreEqual(0, journey.Price);
        }
    }
}
