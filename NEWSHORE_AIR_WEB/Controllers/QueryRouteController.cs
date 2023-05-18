using Microsoft.AspNetCore.Mvc;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_BUSINESS.Entity;
using System.Threading.Tasks;
using NEWSHORE_AIR_API.ViewModel;

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
        [ProducesResponseType(typeof(ResponseBase<>), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task<IActionResult> Get([FromQuery] RouteRequest request)
        {

            try
            {
                _logger.LogTrace("Gettings Routes");
                Journey response = await _iQueryRoute.GetRoute(request);
                if (response.Flights.Count > 0)
                    return Ok(new ResponseBase<Journey>() { StatusCode = 200, Data = response });
                else
                    return Ok(new ResponseBase<string>() { StatusCode = 204, Data = "Su consulta no puede ser procesada" });
            }
            catch (MyCustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<string>() { StatusCode = 500, Data = "Su consulta no puede ser procesada" });
            }
            
        }
    }
}

