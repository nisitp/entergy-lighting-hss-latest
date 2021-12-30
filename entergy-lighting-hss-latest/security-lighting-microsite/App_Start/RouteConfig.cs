using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace security_lighting_microsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "{OpCo}",
                defaults: new { controller = "Home", action = "Index", OpCo = UrlParameter.Optional });

            //[Route("{OpCo}/fixtures")]
            routes.MapRoute(
                name: "Fixtures",
                url: "{OpCo}/fixtures",
                defaults: new { controller = "Home", action = "Fixtures" });

            //[Route("{OpCo}/fixture/{FixtureType}")]
            routes.MapRoute(
                name: "FixtureDetail",
                url: "{OpCo}/fixture/{FixtureType}",
                defaults: new { controller = "Home", action = "Fixture" });

            //[Route("{OpCo}/fixture/{FixtureType}/summary", Name = "FixtureSummary")]
            routes.MapRoute(
                name: "FixtureSummary",
                url: "{OpCo}/fixture/{FixtureType}/summary",
                defaults: new { controller = "Home", action = "FixtureSummary" });

            routes.MapRoute(
                name: "Contact",
                url: "contact/send",
                defaults: new { controller = "Contact", action = "Send" });
        
        }
    }
}
