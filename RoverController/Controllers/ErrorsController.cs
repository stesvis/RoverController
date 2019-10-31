using RoverController.Logger;
using RoverController.Web.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoverController.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorsController : BaseController
    {
        public ErrorsController()
        {
        }

        public ErrorsController(IAppService appService) : base(appService)
        {
        }

        // GET: Errors
        public async Task<ActionResult> Index(Exception exception, string exceptionPath, string details)
        {
            if (!(TempData["exception"] is Exception model))
            {
                model = exception;
            }

            ViewBag.Details = details;

            if (exception != null)
            {
                AppLogger.Logger.Fatal(exception);
            }

            return View(model: model);
        }

        // GET: Errors/NotFound
        public ActionResult NotFound(Exception exception, string exceptionPath, string details)
        {
            if (!(TempData["exception"] is Exception model))
            {
                model = exception;
            }

            ViewBag.Details = details;

            AppLogger.Logger.Error(model, "404 Not Found");

            return View(model: model);
        }

        // GET: Errors/Oops
        public ActionResult Oops(Exception exception, string exceptionPath, string details)
        {
            if (!(TempData["exception"] is Exception model))
            {
                model = exception;
            }

            ViewBag.Details = details;

            AppLogger.Logger.Error(model, "Oops");

            return View(model: model);
        }

        // GET: Errors/Unauthorized
        public ActionResult Unauthorized()
        {
            if (!(TempData["exception"] is Exception model))
            {
                model = new Exception("You are not authorized to view this page.");
            }

            AppLogger.Logger.Error(model, "Unauthorized access");

            return View(model);
        }
    }
}