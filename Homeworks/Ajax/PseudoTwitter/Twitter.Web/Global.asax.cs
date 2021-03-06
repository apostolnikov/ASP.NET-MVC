﻿using System.Data.Entity;

using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

using Twitter.Data;
using Twitter.Data.Migrations;
using Twitter.Web.App_Start;

namespace Twitter.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>());

            MapperConfig.RegisterMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
