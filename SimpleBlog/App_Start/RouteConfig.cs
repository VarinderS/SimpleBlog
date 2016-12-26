using SimpleBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.LowercaseUrls = true;

			var nameSpaces = new string[] { typeof(PostController).Namespace };

			routes.MapRoute(
				name: "PostDetails",
				url: "Post/{id}/{slug}",
				defaults: new { controller = "Post", action = "Details", id = UrlParameter.Optional, slug = UrlParameter.Optional },
				namespaces: nameSpaces
			);

			routes.MapRoute(
				name: "TagDetails",
				url: "Tag/{id}/{slug}/{pageNumber}",
				defaults: new { controller = "Tag", action = "Index", id = UrlParameter.Optional, slug = UrlParameter.Optional, pageNumber = UrlParameter.Optional },
				namespaces: nameSpaces
			);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: nameSpaces
			);
        }
    }
}
