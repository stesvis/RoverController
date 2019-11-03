using RoverController.Repositories;
using RoverController.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace RoverController.Web.Repositories.Missions
{
    public class MissionsRepository : GenericRepository<Mission>, IMissionsRepository
    {
        public ApplicationDbContext ApplicationContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public MissionsRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<Mission> GetAll()
        {
            return ApplicationContext.Missions
                .AsNoTracking()
                .Include(x => x.CreatedByUser)
                .OrderByDescending(x => x.CreatedDate);
        }

        public override Mission GetFull(object id)
        {
            return ApplicationContext.Missions
                .Include(x => x.CreatedByUser)
                .Include(x => x.PinPoints)
                .FirstOrDefault(x => x.Id == (int)id);
        }
    }
}