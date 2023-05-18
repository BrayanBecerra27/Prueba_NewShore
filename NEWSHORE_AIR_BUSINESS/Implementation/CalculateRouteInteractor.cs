﻿using AutoMapper;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Enumerator;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;

namespace NEWSHORE_AIR_BUSINESS.Implementation
{
    public class CalculateRouteInteractor : ICalculateRouteInteractor
    {
        private readonly IMapper _mapper;
        public CalculateRouteInteractor(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<Flight>> CalculateRouteAsync(RouteRequest request, List<RouteResponse> routeResponse)
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
                    flights.AddRange(CreateRoute(routeRequestReturn, routeResponse.Where(s => !s.DepartureStation.Equals(routeRequestReturn.Destination) && !s.ArrivalStation.Equals(routeRequestReturn.Origin)).ToList()));
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
                if (flights.Count > 0 && flights.Any(s => s.Origin.Equals(request.Origin)) && flights.Any(s => s.Destination.Equals(request.Destination)))
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
            routefinish = routeResponse.Where(s => s.ArrivalStation.Equals(request.Destination) && s.DepartureStation.Equals(currentRoute.ArrivalStation)).FirstOrDefault() ?? routefinish;
            if (!string.IsNullOrEmpty(routefinish.DepartureStation))
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
