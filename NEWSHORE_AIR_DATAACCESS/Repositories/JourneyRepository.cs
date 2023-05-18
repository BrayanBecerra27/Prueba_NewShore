using NEWSHORE_AIR_DATAACCESS.Context;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

namespace NEWSHORE_AIR_DATAACCESS.Repositories
{
    public class JourneyRepository : GenericRepository<Journey>, IJourneyRepository
    {
        private readonly NewShoreDbContext _context;
        private readonly ITransportRepository _iTransportRepository;
        private readonly IFlightRepository _iFlightRepository;
        public JourneyRepository(NewShoreDbContext context, ITransportRepository transportRepository, IFlightRepository flightRepository) : base(context)
        {
            _context = context;
            _iTransportRepository = transportRepository;
            _iFlightRepository = flightRepository;
        }

        public async Task< Journey> GetJourneyFromDB(string origin, string destination, string routeType)
        {
            Journey journey = await _context.Journeys.Where(s => s.Origin.Equals(origin) && s.Destination.Equals(destination) && s.RouteType.Equals(routeType)).FirstOrDefaultAsync() ?? new Journey(origin, destination, 0, new List<Flight>());
            if(journey.JourneyId > 0) { 
                journey.Flights = await _context.Flights.Where(s=>s.JourneyId == journey.JourneyId).ToListAsync() ?? new List<Flight>();
                if (journey.Flights.Count > 0)
                {
                    foreach (var item in journey.Flights)
                    {
                        item.Transport = await _context.Transports.Where(s => s.TransportId == item.TransportId).FirstOrDefaultAsync() ?? new Transport(string.Empty, string.Empty);
                    }
                }
            }
            return journey;
        }

        public async Task SaveJourney(Journey journey)
        {
            await AddAsync(journey);
            if(journey.JourneyId > 0) 
            {
                await _iTransportRepository.AddRangeAsync(journey.Flights.Select(s=>s.Transport));
                foreach (var item in journey.Flights)
                {
                    item.TransportId = item.Transport.TransportId;
                    item.JourneyId = journey.JourneyId;

                }

                await _iFlightRepository.AddRangeAsync(journey.Flights);
            }
        }
    }
}
