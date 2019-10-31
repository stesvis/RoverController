using RoverController.Models;
using RoverController.Repositories;
using RoverController.Web.Models;
using System.Data.Entity;

namespace RoverController.Web.Repositories.Positions
{
    public class PositionsRepository : GenericRepository<Position>, IPositionsRepository
    {
        public ApplicationDbContext ApplicationContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public PositionsRepository(DbContext context) : base(context)
        {
        }
    }
}