using RoverController.Repositories;
using RoverController.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace RoverController.Web.Repositories.MissionAttachments
{
    public class MissionAttachmentsRepository : GenericRepository<MissionAttachment>, IMissionAttachmentsRepository
    {
        public ApplicationDbContext ApplicationContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public MissionAttachmentsRepository(DbContext context) : base(context)
        {
        }

        public override IQueryable<MissionAttachment> GetAll()
        {
            return ApplicationContext.MissionAttachments
                .AsNoTracking()
                .Include(x => x.CreatedByUser)
                .OrderByDescending(x => x.CreatedDate);
        }

        public override MissionAttachment GetFull(object id)
        {
            return ApplicationContext.MissionAttachments
                .Include(x => x.CreatedByUser)
                .FirstOrDefault(x => x.Id == (int)id);
        }
    }
}