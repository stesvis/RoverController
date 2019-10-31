using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using RoverController.Web.Controllers;
using RoverController.Web.DependencyResolvers;
using RoverController.Web.Models;
using RoverController.Web.Services;
using RoverController.Web.Services.PositionsService;
using RoverController.Web.Services.Users;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

[assembly: OwinStartupAttribute(typeof(RoverController.Web.Startup))]

namespace RoverController.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var services = new ServiceCollection();
            IUnityContainer unityContainer = new UnityContainer();
            ConfigureServices(services, unityContainer);
            DependencyResolver.SetResolver(new WebDependencyResolver(unityContainer));

            MvcUnityContainer.Container = unityContainer;
            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
        }

        private void ConfigureServices(IServiceCollection services, IUnityContainer unityContainer)
        {
            // Services
            unityContainer.RegisterType<IAppService, AppService>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IPositionsService, PositionsService>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());

            unityContainer.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

            // Controllers
            unityContainer.RegisterType<BaseController>(new InjectionConstructor(typeof(IAppService)));
            unityContainer.RegisterType<AccountController>(new InjectionConstructor(typeof(IAppService)));
            unityContainer.RegisterType<ManageController>(new InjectionConstructor(typeof(IAppService)));
        }
    }
}