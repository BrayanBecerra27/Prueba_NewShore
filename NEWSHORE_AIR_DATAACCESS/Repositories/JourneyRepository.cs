using NEWSHORE_AIR_BUSINESS.Context;
using NEWSHORE_AIR_BUSINESS.Entity;
using NEWSHORE_AIR_BUSINESS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWSHORE_AIR_DATAACCESS.Repositories
{
    public class JourneyRepository : GenericRepository<Journey>, IJourneyRepository
    {
        private readonly NewShoreDbContext _context;
        public JourneyRepository(NewShoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
