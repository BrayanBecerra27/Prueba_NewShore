using NEWSHORE_AIR_BUSINESS.Interface;
using NEWSHORE_AIR_BUSINESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NEWSHORE_AIR_BUSINESS.Enumerator;
using AutoMapper;

namespace NEWSHORE_AIR_DATAACCESS.Implementation
{
    public  class FlightService : IFlightService
    {
        public FlightService() { 

        }  

        public async Task<List<RouteResponse>> GetInformationRoutesAsync(RouteRequest request)
        {
            List<RouteResponse> routeResponse = new List<RouteResponse>();
            using (var httpClient = new HttpClient())
            {
                string routeType = MapRouteType(request.RouteType);
                var response = await httpClient.GetAsync($"https://recruiting-api.newshore.es/api/flights/{routeType}?origin={request.Origin}&destination={request.Destination}");
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                routeResponse = JsonConvert.DeserializeObject<List<RouteResponse>>(await response.Content.ReadAsStringAsync()) ?? routeResponse;
                return routeResponse;
            }
        }

        private string MapRouteType(RouteType routeType)
        {
            string routeTypeInt = string.Empty;
            switch (routeType)
            {
                case RouteType.Unique:
                    routeTypeInt = "0";
                    break;
                case RouteType.Multiple:
                    routeTypeInt = "1";
                    break;
                case RouteType.MultipleAndReturn:
                    routeTypeInt = "2";
                    break;
                default:
                    routeTypeInt = "0";
                    break;
            }
            return routeTypeInt;
        }
    }
}
