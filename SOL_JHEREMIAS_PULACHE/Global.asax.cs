using SOL_JHEREMIAS_PULACHE.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SOL_JHEREMIAS_PULACHE
{
    public class MvcApplication : System.Web.HttpApplication
    {
        HelperUpdateUser helperUpdateUser = new HelperUpdateUser();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            helperUpdateUser.StartScheduledTask();
        }
    }
}
