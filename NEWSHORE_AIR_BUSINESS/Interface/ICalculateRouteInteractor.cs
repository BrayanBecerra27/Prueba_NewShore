using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Models;

namespace NEWSHORE_AIR_BUSINESS.Interface
{
    public interface ICalculateRouteInteractor
    {
        Task<List<Flight>> CalculateRouteAsync(RouteRequest request, List<RouteResponse> routeResponse);
    }
}
