using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AV.Set.Infrustructure;
using AV.Set.Infrustructure.Services;
using AV.Set.Model;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace AV.Web.Set.Attributes
{
    public class PushUserStatusActiveAttribute: ActionFilterAttribute
    {
        [Dependency]
        public IMvcApplication MvcApp { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
                var user = MvcApp.ActiveUsers.FirstOrDefault(u => u.Name == identity.Name);
                var userExistAtApp = user != null;
                if (!userExistAtApp)
                {
                    // restore user and player
                    var restoredUser = new User() { Name = identity.Name };
                    MvcApp.ActiveUsers.Add(restoredUser);
                    MvcApp.CurrentGame.Players.Add(new Player() { User = restoredUser, State = PlayerState.Active });
                }
            }
        }
    }
}