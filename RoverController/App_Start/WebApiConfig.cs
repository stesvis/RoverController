using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Serialization;
using RoverController.Web.API.Controllers;
using RoverController.Web.Mapper;
using RoverController.Web.Models;
using RoverController.Web.Services;
using RoverController.Web.Services.PositionsService;
using RoverController.Web.Services.Users;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace RoverController
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionBased",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // These two lines fix some Exceptions when returning complex JSON objects
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            var unityContainer = new UnityContainer();

            // Services
            unityContainer.RegisterType<IAppMapper, AppMapper>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IAppService, AppService>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IPositionsService, PositionsService>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());

            unityContainer.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

            // Controllers
            unityContainer.RegisterType<BaseApiController>(new InjectionConstructor(typeof(IAppService)));
            unityContainer.RegisterType<AccountController>(new InjectionConstructor(typeof(IAppService)));

            config.DependencyResolver = new ApiDependencyResolver(unityContainer);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}