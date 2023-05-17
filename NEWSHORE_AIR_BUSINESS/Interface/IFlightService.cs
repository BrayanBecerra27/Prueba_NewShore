using NEWSHORE_AIR_BUSINESS.Models;

namespace NEWSHORE_AIR_BUSINESS.Interface
{
    public interface IFlightService
    {
        Task<List<RouteResponse>> GetInformationRoutesAsync(RouteRequest request);
    }
}
