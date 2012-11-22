using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AV.Set.Infrustructure;
using AV.Set.Model;

namespace AV.Web.Set
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IMvcApplication
    {
        public MvcApplication()
        {
            ActiveUsers = new List<User>();    
            CurrentGame = new Game();
        }

        protected void Application_Start()
        {
            Bootstrapper.Initialise(this);

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public ICollection<User> ActiveUsers { get; private set; }
        public Game CurrentGame { get; private set; }
        public void StartNewGame()
        {
            CurrentGame = new Game();
        }
    }
}