using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using AV.Set.DataModule;
using AV.Set.Infrustructure;
using AV.Web.Set.Attributes;
using AV.Web.Set.Binders;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace AV.Web.Set
{
    public static class Bootstrapper
    {
        public static void Initialise(IMvcApplication application)
        {
            //Database.SetInitializer<SetGameContext>(new SetGameDbInitializer());
            var container = BuildUnityContainer(application);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer(IMvcApplication application)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterInstance<IMvcApplication>(application);
            container.RegisterInstance<IModelBinderProvider>(new SetModelBinderProvider());

            RegisterModules(container);

            return container;
        }

        private static void RegisterModules(UnityContainer container)
        {
            container.Resolve<AV.Set.CoreServices.Module>().Register();
        }
    }
}