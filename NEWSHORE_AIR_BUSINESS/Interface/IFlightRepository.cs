using NEWSHORE_AIR_BUSINESS.Entity;

namespace NEWSHORE_AIR_BUSINESS.Interface
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<List<Flight>> GetFlightsByJourneyIdAsync(int journeyId);
    }
}
