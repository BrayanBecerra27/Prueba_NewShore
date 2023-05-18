using Microsoft.AspNetCore.Mvc;
using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_API.ViewModel;
using NEWSHORE_AIR_BUSINESS.ViewModel;
using Microsoft.AspNetCore.Routing;
using static AutoMapper.Internal.ExpressionFactory;

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
        /// <summary>
        /// Gets the route based on the origin, destination, type of route and optionally the number of scales
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetRoute")]
        [ProducesResponseType(typeof(ResponseBase<>), StatusCodes.Status200OK, contentType: "application/json")]
        [ProducesResponseType(typeof(ResponseBase<>), StatusCodes.Status204NoContent, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest, contentType: "application/json")]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError, contentType: "application/json")]
        public async Task<IActionResult> Get([FromQuery] RouteRequest request)
        {

            try
            {

                JourneyResponse response = await _iQueryRoute.GetRoute(request);
                if (response.Flights.Count > 0)
                    return Ok(new ResponseBase<JourneyResponse>() { StatusCode = 200, Data = response });
                else
                    return StatusCode(204, (new ResponseBase<string>() { StatusCode = 204, Data = "Su consulta no puede ser procesada" }));
            }
            catch (MyCustomException ex)
            {
                _logger.LogError($"Error Gettings Routes: {ex}");
                throw new MyCustomException("Error Gettings Routes", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Gettings Routes: {ex}");
                return StatusCode(500, (new ResponseBase<string>() { StatusCode = 500, Data = "Su consulta no puede ser procesada" }));
            }
            
        }
    }
}

