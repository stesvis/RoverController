using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RoverController.Lib;
using RoverController.Logger;
using RoverController.Web.Helper;
using RoverController.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoverController.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Properties

        public IAppService AppService { get; }

        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        #endregion Properties

        public BaseController()
        {
            ViewBag.Errors = new List<string>();
        }

        public BaseController(IAppService appService)
        {
            AppService = appService;
            ViewBag.Errors = new List<string>();
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAppService appService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AppService = appService;

            ViewBag.Errors = new List<string>();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // Just in case I forget to handle some exceptions in the controllers
            base.OnException(filterContext);
            (ViewBag.Errors as List<string>).Add(filterContext.Exception.Message);
            AppLogger.Logger.Error(filterContext.Exception);
        }

        /// <summary>
        /// Returns a list of ModelState error
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public IList<string> GetErrors(ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage + " " + v.Exception)
                .ToList();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var requestLog = Request.Url.PathAndQuery;

                if (User != null)
                {
                    var currentUserId = User.Identity.GetUserId();

                    if (!currentUserId.IsEmpty())
                    {
                        requestLog += $" | UserId: {currentUserId}";

                        if (AppService.CurrentUser != null && !AppService.CurrentUser.UserName.IsEmpty())
                        {
                            requestLog += $" | Username: {AppService.CurrentUser.UserName}";
                        }
                        else
                        {
                            requestLog += $" | Username: unknown";
                        }

                        ViewHelper.IsAdmin = UserIsAtLeast(currentUserId, UserRoles.Admin);

                        ViewHelper.CurrentUserId = currentUserId;
                    }
                    else
                    {
                        requestLog += $" | UserId: unknown";
                        AppLogger.Logger.Debug($"Resetting ViewHelper");

                        ViewHelper.IsSuperadmin = false;
                        ViewHelper.IsAdmin = false;
                        ViewHelper.IsUser = false;

                        ViewHelper.CurrentUserId = string.Empty;
                    }
                }

                AppLogger.Logger.Trace(requestLog);
            }
            catch (Exception ex)
            {
                // do nothing, silently continue
                AppLogger.Logger.Error(ex, "Something went wrong in BaseController.");
            }
            finally
            {
                base.OnActionExecuting(filterContext);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }
            }

            base.Dispose(disposing);
        }

        public bool UserIsAtLeast(string userId, string minimumRole)
        {
            bool retval = false;

            try
            {
                switch (minimumRole)
                {
                    case UserRoles.Admin:
                        retval = UserIsInRoles(userId, UserRoles.Admin);

                        break;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw;
            }

            return retval;
        }

        public bool UserIsInRoles(string userId, params string[] roles)
        {
            //using (var userManager = UserManager)
            //{
            return roles.ToList().Any(role => UserManager.IsInRole(userId, role));
            //}
        }
    }
}