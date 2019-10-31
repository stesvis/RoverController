using RoverController.Web.Services;
using System.Web.Mvc;

namespace RoverController.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IAppService appService) : base(appService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}