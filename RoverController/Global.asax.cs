using RoverController.Logger;
using RoverController.Web.Mapper;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RoverController
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
#if true
            var httpContext = ((MvcApplication)sender).Context;

            //var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            //var currentController = " ";
            //var currentAction = " ";

            //if (currentRouteData != null)
            //{
            //    if (currentcontroller != null &&
            //        !String.IsNullOrEmpty(currentcontroller.ToString()))
            //    {
            //        currentController = currentcontroller.ToString();
            //    }

            //    if (currentaction != null &&
            //        !String.IsNullOrEmpty(currentaction.ToString()))
            //    {
            //        currentAction = currentaction.ToString();
            //    }
            //}

            var ex = Server.GetLastError();

            AppLogger.Logger.Fatal(ex);

            var controller = "Errors";
            var action = "Index";
            var exceptionPath = string.Empty;

            httpContext.ClearError();
            httpContext.Response.Clear();

            if (ex is HttpException)
            {
                var httpEx = ex as HttpException;

                switch (httpEx.GetHttpCode())
                {
                    case 404:
                        controller = "Errors";
                        action = "NotFound";

                        break;

                    // others if any
                    case 401:
                        controller = "Errors";
                        action = "Unauthorized";

                        break;

                    default:
                        controller = "Errors";
                        action = "Oops";
                        break;
                }
            }
            else if (ex is InvalidOperationException)
            {
                controller = "Errors";
                action = "Oops";
            }
            else if (ex is System.Data.Entity.Core.EntityException)
            {
                controller = "Errors";
                action = "Oops";
                if (ex.Message.Contains("The underlying provider failed on Open"))
                {
                    try
                    {
                        // Email me the error right away, or send me an sms
                        var alertPhoneNumber = Environment.GetEnvironmentVariable("ALERT_PHONE_NUMBER");
                    }
                    catch (Exception alertEx)
                    {
                        // just log it for now
                        AppLogger.Logger.Error(alertEx);
                    }
                }
            }
            else
            {
                httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
                httpContext.Response.TrySkipIisCustomErrors = true;
                controller = "Errors";
                action = "Index";

                exceptionPath = HttpContext.Current.Request.Url.AbsolutePath;
            }

            HttpContext.Current.Response.RedirectToRoute(controller.ToString(), new { action, details = ex.Message });
#else
            Exception exception = Server.GetLastError();
            Server.ClearError();

            var routeData = new RouteData();
            controller = "Home";
            action = "Index";
            routeData.Values["exception"] = exception;

            if (exception.GetType() == typeof(HttpException))
            {
                Response.StatusCode = ((HttpException)exception).GetHttpCode();
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                Response.StatusCode = 500;
                routeData.Values.Add("statusCode", 500);
            }

            IController controller = new ErrorsController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            controller.Execute(rc);
#endif
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }
    }
}