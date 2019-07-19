using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace derrick_blog_app
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {



            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //route for navigating posts
            routes.MapRoute(
                name: "NewSlug",
                //You can change 'Blog' in the URL to whatever you want (Vanity URL)
                url: "Blog/{slug}",
                defaults: new
                {
                    controller = "BlogPosts",
                    action = "Details",
                    slug = UrlParameter.Optional
                });
            //default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
