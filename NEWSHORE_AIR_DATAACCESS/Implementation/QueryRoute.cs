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
        public QueryRoute(IMapper mapper, IFlightService flightService, IJourneyRepository iJourneyRepository)
        {
            _mapper = mapper;
            _iFlightService = flightService;
            _iJourneyRepository = iJourneyRepository;
        }

        public async Task<Journey> GetRoute(RouteRequest request)
        {
            Journey journey = await _iJourneyRepository.GetJourneyFromDB(request.Origin, request.Destination, request.RouteType.ToString());
            if(journey.Flights.Count() >0)
                return journey;
            else
            {
                List<RouteResponse> routeResponse = await _iFlightService.GetInformationRoutesAsync(request);
                if (routeResponse.Count > 0)
                {
                    journey.Flights = CalculateRouteAsync(request, routeResponse);
                    journey.Price = journey.Flights.Sum(f => f.Price);
                    if(journey.Flights.Count > 0)
                    {
                        journey.RouteType = request.RouteType.ToString(); 
                        await _iJourneyRepository.SaveJourney(journey);
                    }
                       
                }
                return journey;
            }
        }

        private List<Flight> CalculateRouteAsync(RouteRequest request, List<RouteResponse> routeResponse)
        {
            List<Flight> flights = new List<Flight>();
            request.Scale = request.Scale == 0 ? 4 : request.Scale = 0;
            switch (request.RouteType)
            {
                case RouteType.Unique:
                    flights = CreateRouteUnique(routeResponse, request); 
                    break;
                case RouteType.Multiple:
                    flights = CreateRoute(request, routeResponse);
                    break;
                case RouteType.MultipleAndReturn:
                    flights = CreateRoute(request, routeResponse.Where(s => !s.DepartureStation.Equals(request.Destination) && !s.ArrivalStation.Equals(request.Origin)).ToList());
                    RouteRequest routeRequestReturn = CreateRequest(request);
                    flights.AddRange(CreateRoute(routeRequestReturn, routeResponse.Where(s => !s.DepartureStation.Equals(routeRequestReturn.Destination) &&  !s.ArrivalStation.Equals(routeRequestReturn.Origin)).ToList()));
                    break;
                default:
                    break;
            }
            return flights;
        }

        private List<Flight> CreateRouteUnique(List<RouteResponse> routeResponse, RouteRequest request)
        {
            return _mapper.Map<List<Flight>>(routeResponse.Where(s => s.DepartureStation.Equals(request.Origin) && s.ArrivalStation.Equals(request.Destination)).ToList());
        }

        private RouteRequest CreateRequest(RouteRequest request)
        {
            return new RouteRequest()
            {
                Origin = request.Destination,
                Destination = request.Origin,
                RouteType = request.RouteType,  
                Scale = request.Scale
            };
        }

        private List<Flight> CreateRoute(RouteRequest request, List<RouteResponse> routeResponse)
        {
            List<Flight> flights = new List<Flight>();
            flights = CreateRouteUnique(routeResponse, request);
            if (flights.Count > 0)
            {
                return flights; 
            }
            List<RouteResponse> routeWithOrigin = routeResponse.Where(s => s.DepartureStation.Equals(request.Origin)).ToList();
            foreach (var item in routeWithOrigin)
            {
                flights.Add(_mapper.Map<Flight>(item));
                flights.AddRange(CreateRecursiveRoute(request, routeResponse, item));
                if (flights.Count > 0 && flights.Any(s=>s.Origin.Equals(request.Origin)) && flights.Any(s=>s.Destination.Equals(request.Destination)))
                    return flights;
                else
                    flights = new List<Flight>();
            }
            return flights;
        }

        private List<Flight> CreateRecursiveRoute(RouteRequest request, List<RouteResponse> routeResponse, RouteResponse currentRoute)
        {
            
            List<Flight> flights = new List<Flight>();
            RouteResponse routefinish = new RouteResponse();
            routefinish =  routeResponse.Where(s => s.ArrivalStation.Equals(request.Destination) && s.DepartureStation.Equals(currentRoute.ArrivalStation)).FirstOrDefault() ?? routefinish;
            if(!string.IsNullOrEmpty(routefinish.DepartureStation))
            {
                flights.Add(_mapper.Map<Flight>(routefinish));
            }
            else
            {
                
                List<RouteResponse> routeOptional = routeResponse.Where(s => s.DepartureStation.Equals(currentRoute.ArrivalStation) && !s.ArrivalStation.Equals(currentRoute.DepartureStation)).ToList();
                foreach (var item in routeOptional)
                {
                    if (request.Scale > 0)
                    {
                        request.Scale -= 1;
                        if (item.ArrivalStation.Equals(request.Destination))
                        {
                            flights.Add(_mapper.Map<Flight>(item));
                            return flights;
                        }
                        else
                        {
                            flights.Add(_mapper.Map<Flight>(item));
                            var countFlights = flights.Count();
                            flights.AddRange(CreateRecursiveRoute(request, routeResponse, item));
                            if (countFlights == flights.Count())
                                flights.Remove(flights[countFlights - 1]);

                        }
                    }

                }
            }
            return flights;
        }
        
    }
}
