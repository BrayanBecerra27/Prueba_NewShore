using Microsoft.EntityFrameworkCore;
using NEWSHORE_AIR_BUSINESS.Entity;

namespace NEWSHORE_AIR_BUSINESS.Context
{
    public  class NewShoreDbContext : DbContext
    {
        public NewShoreDbContext(DbContextOptions<NewShoreDbContext> options) : base (options)
        {

        }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Transport> Transports { get; set; }
    }
}
