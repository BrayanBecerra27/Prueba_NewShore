using NEWSHORE_AIR_BUSINESS.Entity;
using AutoMapper;
using NEWSHORE_AIR_BUSINESS.Models;

namespace NEWSHORE_AIR_BUSINESS.Mapper
{
    public  class MappingFlight : Profile
    {
        public MappingFlight()
        {
            CreateMap<RouteResponse, Transport>()
                .ForMember(
                   dest => dest.FlightCarrier,
                   opt => opt.MapFrom(src => $"{src.FlightCarrier}")
               ).ForMember(
                   dest => dest.FlightNumber,
                   opt => opt.MapFrom(src => $"{src.FlightNumber}")
               );
            CreateMap<RouteResponse, Flight>()
               .ForMember(
                   dest => dest.Destination,
                   opt => opt.MapFrom(src => $"{src.ArrivalStation}")
               )
               .ForMember(
                   dest => dest.Origin,
                   opt => opt.MapFrom(src => $"{src.DepartureStation}")
               )
               .ForMember(
                   dest => dest.Price,
                   opt => opt.MapFrom(src => $"{src.Price}")
               )
               .ForMember(
                   dest => dest.Transport,
                   opt => opt.MapFrom(src => src)
               );
            CreateMap<Transport, TransportResponse>()
               .ForMember(
                   dest => dest.FlightNumber,
                   opt => opt.MapFrom(src => $"{src.FlightNumber}")
               )
               .ForMember(
                   dest => dest.FlightCarrier,
                   opt => opt.MapFrom(src => $"{src.FlightCarrier}")
               );
            CreateMap<Flight, FlightResponse>()
                .ForMember(
                   dest => dest.Destination,
                   opt => opt.MapFrom(src => $"{src.Destination}")
               )
               .ForMember(
                   dest => dest.Origin,
                   opt => opt.MapFrom(src => $"{src.Origin}")
               )
               .ForMember(
                   dest => dest.Price,
                   opt => opt.MapFrom(src => $"{src.Price}")
               )
               .ForMember(
                   dest => dest.Transport,
                   opt => opt.MapFrom(src => $"{src}")
               );
            CreateMap<Journey, JourneyResponse>()
                .ForMember(
                   dest => dest.Destination,
                   opt => opt.MapFrom(src => $"{src.Destination}")
               )
               .ForMember(
                   dest => dest.Origin,
                   opt => opt.MapFrom(src => $"{src.Origin}")
               )
               .ForMember(
                   dest => dest.Price,
                   opt => opt.MapFrom(src => $"{src.Price}")
               )
               .ForMember(
                   dest => dest.Flights,
                   opt => opt.MapFrom(src => $"{src}")
               );
        }
    }
}
