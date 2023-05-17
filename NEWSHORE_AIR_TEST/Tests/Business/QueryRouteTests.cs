using AutoMapper;
using Moq;
using NEWSHORE_AIR_BUSINESS.Implementation;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_BUSINESS.Enumerator;
using NEWSHORE_AIR_DATAACCESS.Entity;
using NEWSHORE_AIR_BUSINESS.Mapper;

namespace NEWSHORE_AIR_TEST.Tests.Businness
{
    [TestFixture]
    public class QueryRouteTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Configurar AutoMapper si es necesario
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingFlight>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Test]
        public async Task GetRoute_ValidRequestWithFlights_ReturnsJourney()
        {
            // Arrange
            var queryRoute = new QueryRoute(_mapper);
            var request = new RouteRequest
            {
                Origin = "Origin",
                Destination = "Destination",
                RouteType = RouteType.Unique,
                Scale = 0
            };
            var routeResponse = new List<RouteResponse> { new RouteResponse() };

            // Act
            var result = await queryRoute.GetRoute(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.Origin, result.Origin);
            Assert.AreEqual(request.Destination, result.Destination);
            Assert.AreEqual(0, result.Price);
            Assert.AreEqual(0, result.Flights?.Count);
        }

       

    }
}

