using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using RoverController.Web.Services;
using Microsoft.AspNet.Identity.Owin;

namespace RoverController.Web.API.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        #region Properties

        public IAppService AppService { get; }

        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
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
                if (_userManager == null)
                {
                    _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion Properties

        public BaseApiController(IAppService appService)
        {
            AppService = appService;
        }

        /// <summary>
        /// Returns a list of ModelState error
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public IList<string> GetErrors(ModelStateDictionary modelState)
        {
            try
            {
                return modelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage + " " + v.Exception)
                        .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}