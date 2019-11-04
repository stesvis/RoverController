using RoverController.Repositories;
using RoverController.Web.Models;
using System.Data.Entity;
using System.Linq;

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

        public override IQueryable<PinPoint> GetAll()
        {
            return ApplicationContext.PinPoints
                .AsNoTracking()
                .Include(x => x.CreatedByUser)
                .OrderByDescending(x => x.CreatedDate);
        }

        public override PinPoint GetFull(object id)
        {
            return ApplicationContext.PinPoints
                .AsNoTracking()
                .Include(x => x.CreatedByUser)
                .FirstOrDefault(x => x.Id == (int)id);
        }
    }
}