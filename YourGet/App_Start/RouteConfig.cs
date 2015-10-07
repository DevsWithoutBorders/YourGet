using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YourGet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterUIRoutes(routes);
            RouteConfig.RegisterNuGetApiV2Routes(routes);
        }

        public static void RegisterUIRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void RegisterNuGetApiV2Routes(RouteCollection routes)
        {
            // V2 routes
            routes.MapRoute(
                RouteName.Team,
                "api/v2/team",
                defaults: new { controller = "Api", action = "Team" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                "v2" + RouteName.VerifyPackageKey,
                "api/v2/verifykey/{id}/{version}",
                new
                {
                    controller = "Api",
                    action = "VerifyPackageKey",
                    id = UrlParameter.Optional,
                    version = UrlParameter.Optional
                });

            routes.MapRoute(
                "v2CuratedFeeds" + RouteName.DownloadPackage,
                "api/v2/curated-feeds/package/{id}/{version}",
                defaults: new { controller = "Api", action = "GetPackageApi", version = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                "v2" + RouteName.DownloadPackage,
                "api/v2/package/{id}/{version}",
                defaults: new { controller = "Api", action = "GetPackageApi", version = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                "v2" + RouteName.PushPackageApi,
                "api/v2/package",
                defaults: new { controller = "Api", action = "PushPackageApi" },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") });

            routes.MapRoute(
                "v2" + RouteName.DeletePackageApi,
                "api/v2/package/{id}/{version}",
                new { controller = "Api", action = "DeletePackageApi" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") });

            routes.MapRoute(
                "v2" + RouteName.PublishPackageApi,
                "api/v2/package/{id}/{version}",
                new { controller = "Api", action = "PublishPackageApi" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRoute(
                "v2PackageIds",
                "api/v2/package-ids",
                new { controller = "Api", action = "PackageIDs" });

            routes.MapRoute(
                "v2PackageVersions",
                "api/v2/package-versions/{id}",
                new { controller = "Api", action = "PackageVersions" });

            routes.MapRoute(
                RouteName.StatisticsDownloadsApi,
                "api/v2/stats/downloads/last6weeks",
                defaults: new { controller = "Api", action = "StatisticsDownloadsApi" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                RouteName.ServiceAlert,
                "api/v2/service-alert",
                defaults: new { controller = "Api", action = "ServiceAlert" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            routes.MapRoute(
                RouteName.Status,
                "api/status",
                new { controller = "Api", action = "StatusApi" });

            routes.MapRoute(
                RouteName.DownloadNuGetExe,
                "nuget.exe",
                new { controller = "Api", action = "GetNuGetExeApi" });
        }
    }
}
