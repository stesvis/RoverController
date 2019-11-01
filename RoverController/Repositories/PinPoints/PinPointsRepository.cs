using RoverController.Repositories;
using RoverController.Web.Models;
using System.Data.Entity;

namespace RoverController.Web.Repositories.PinPoints
{
    public class PinPointsRepository : GenericRepository<PinPoint>, IPinPointsRepository
    {
        public ApplicationDbContext ApplicationContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public PinPointsRepository(DbContext context) : base(context)
        {
        }
    }
}