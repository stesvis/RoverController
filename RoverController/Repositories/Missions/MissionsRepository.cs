using RoverController.Models;
using RoverController.Repositories;
using RoverController.Web.Models;
using System.Data.Entity;

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
    }
}