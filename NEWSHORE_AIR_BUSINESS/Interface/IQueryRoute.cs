using NEWSHORE_AIR_BUSINESS.Models;

namespace NEWSHORE_AIR_BUSINESS.Interface
{
    public interface IQueryRoute 
    {
        Task<JourneyResponse> GetRoute(RouteRequest request);
    }
}
