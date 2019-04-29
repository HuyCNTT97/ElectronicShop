using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
       new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "Content ",
                url: "tin-tuc/{metatitle}-{id}",
                defaults: new { controller = "News", action = "DetailContent", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "OrderDetail ",
                url: "quan-ly-hoa-don/hoa-don-{id}",
                defaults: new { controller = "Order", action = "Order_Detail", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "About Us ",
                url: "gioi-thieu",
                defaults: new { controller = "Contact", action = "AboutUs", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
               name: "List Product",
               url: "san-pham",
               defaults: new { controller = "Product", action = "ListProduct", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
               name: "Register",
               url: "dang-ki",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
                name: "List News",
                url: "tin-tuc-{metatitle}-{id}",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );

            routes.MapRoute(
               name: "Complete Shopping",
               url: "hoan-thanh",
               defaults: new { controller = "Cart", action = "Complete", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "ShopOnline.Controllers" }
            );
            routes.MapRoute(
               name: "Remove Cart",
               url: "remove-gio-hang",
               defaults: new { controller = "Cart", action = "RemoveCart", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
              name: "Payment ",
              url: "thanh-toan-gio-hang",
              defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
          );
            routes.MapRoute(
               name: "Update Cart",
               url: "removeall-gio-hang",
               defaults: new { controller = "Cart", action = "DeleteAll", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
               name: " Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "UpdateQuantity", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
              name: " Information",
              url: "quan-li-tai-khoan",
              defaults: new { controller = "Home", action = "Information", id = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
          );
            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
            routes.MapRoute(
              name: "Order",
              url: "quan-ly-hoa-don-{count}",
              defaults: new { controller = "Order", action = "ManagerOrder", count = UrlParameter.Optional },
              namespaces: new[] { "ShopOnline.Controllers" }
          );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "ShopOnline.Controllers" }
           );
        }
    }
}
