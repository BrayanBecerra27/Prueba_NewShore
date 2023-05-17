using NEWSHORE_AIR_BUSINESS.Models;
using NEWSHORE_AIR_BUSINESS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_BUSINESS.Interface
{
    public interface IQueryRoute 
    {
        Task<Journey> GetRoute(RouteRequest request);
    }
}
