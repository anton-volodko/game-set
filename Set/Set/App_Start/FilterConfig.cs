using System.Web;
using System.Web.Mvc;
using AV.Web.Set.Attributes;

namespace AV.Web.Set
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            var attr = DependencyResolver.Current.GetService<PushUserStatusActiveAttribute>();
            filters.Add(attr);
        }
    }
}