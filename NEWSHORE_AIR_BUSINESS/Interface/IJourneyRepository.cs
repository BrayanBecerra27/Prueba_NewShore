using NEWSHORE_AIR_BUSINESS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_BUSINESS.Interface
{
    public interface IJourneyRepository : IGenericRepository<Journey>
    {
        Task<Journey> GetJourneyFromDB(string origin, string destination, string routeType);
        Task SaveJourney(Journey journey);
    }
}
