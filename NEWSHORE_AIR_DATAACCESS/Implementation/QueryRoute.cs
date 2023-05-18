using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using Newtonsoft.Json;
using NEWSHORE_AIR_BUSINESS.Enumerator;
using AutoMapper;
using System.Data;
using NEWSHORE_AIR_BUSINESS.Entity;

namespace NEWSHORE_AIR_DATAACCESS.Implementation
{
    public class QueryRoute : IQueryRoute
    {
        private readonly IMapper _mapper;
        private readonly IFlightService _iFlightService;
        private readonly IJourneyRepository _iJourneyRepository;
        private readonly ICalculateRouteInteractor _iCalculateRouteInteractor;
        public QueryRoute(IMapper mapper,IFlightService flightService, IJourneyRepository iJourneyRepository, ICalculateRouteInteractor calculateRoute)
        {
            _iFlightService = flightService;
            _iJourneyRepository = iJourneyRepository;
            _iCalculateRouteInteractor = calculateRoute;
            _mapper = mapper;
        }

        public async Task<JourneyResponse> GetRoute(RouteRequest request)
        {
            Journey journey = await _iJourneyRepository.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString());
            if(journey.Flights.Count() == 0)
            {
                List<RouteResponse> routeResponse = await _iFlightService.GetInformationRoutesAsync(request);
                if (routeResponse.Count > 0)
                {
                    journey.Flights = await _iCalculateRouteInteractor.CalculateRouteAsync(request, routeResponse);
                    journey.Price = journey.Flights.Sum(f => f.Price);
                    if(journey.Flights.Count > 0)
                    {
                        journey.RouteType = request.RouteType.ToString(); 
                        await _iJourneyRepository.SaveJourney(journey);
                    }
                }
            }
            return _mapper.Map<JourneyResponse>(journey);
        }
    }
}
