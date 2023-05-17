using Microsoft.AspNetCore.Mvc;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_BUSINESS.Entity;

namespace NEWSHORE_AIR_WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryRouteController : ControllerBase
    {
        
        private readonly ILogger<QueryRouteController> _logger;
        private readonly IQueryRoute _iQueryRoute;

        public QueryRouteController(ILogger<QueryRouteController> logger, IQueryRoute queryRoute)
        {
            _logger = logger;
            _iQueryRoute = queryRoute;
        }

        [HttpGet(Name = "GetRoute")]
        public async Task<IActionResult> Get([FromQuery] RouteRequest request)
        {
            try
            {
                Journey response = await _iQueryRoute.GetRoute(request);
                if (response.Flights.Count > 0)
                    return Ok(response);
                else
                    return Ok("Su consulta no puede ser procesada");
                
            }
            catch (Exception ex)
            {
                return BadRequest("Su consulta no puede ser procesada");
            }
        }
    }
}

