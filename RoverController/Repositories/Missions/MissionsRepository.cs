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
                .Include(x => x.CreatedByUser)
                .Include(x => x.Attachments)
                .OrderByDescending(x => x.CreatedDate);
        }

        public override Mission GetFull(object id)
        {
            return ApplicationContext.Missions
                .AsNoTracking()
                .Include(x => x.CreatedByUser)
                .Include(x => x.PinPoints)
                .Include(x => x.Attachments)
                .FirstOrDefault(x => x.Id == (int)id);
        }
    }
}