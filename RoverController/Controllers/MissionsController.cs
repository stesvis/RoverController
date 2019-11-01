using RoverController.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoverController.Web.Controllers
{
    public class MissionsController : BaseController
    {
        public MissionsController(IAppService appService) : base(appService)
        {
        }

        // GET: Missions
        public ActionResult Index()
        {
            var missionDTOs = AppService.Missions.All();
            return View(missionDTOs);
        }
    }
}