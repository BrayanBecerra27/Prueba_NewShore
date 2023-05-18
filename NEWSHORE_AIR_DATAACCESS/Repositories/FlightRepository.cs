using NEWSHORE_AIR_DATAACCESS.Context;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NEWSHORE_AIR_DATAACCESS.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly NewShoreDbContext _context;
        public FlightRepository(NewShoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetFlightsByJourneyIdAsync(int journeyId)
        {
            return await _context.Flights.Where(s => s.JourneyId == journeyId).ToListAsync() ?? new List<Flight>();
        }
    }
}
