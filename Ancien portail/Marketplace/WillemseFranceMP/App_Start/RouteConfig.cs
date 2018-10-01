using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WillemseFranceMP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /* Pour mes Arborescence */

            routes.MapRoute("Arb4List", "Produits/Arb4/List/{Arb1Code}/{Arb2Code}/{Arb3Code}", new { controller = "Produits", action = "Arb4List", Arb1Code = "", Arb2Code = "", Arb3Code = "" });

            routes.MapRoute("Arb3List", "Produits/Arb3/List/{Arb1Code}/{Arb2Code}", new { controller = "Produits", action = "Arb3List", Arb1Code = "", Arb2Code ="" });

            routes.MapRoute("Arb2List","Produits/Arb2/List/{Arb1Code}",new { controller = "Produits", action = "Arb2List", Arb1Code = "" });

            routes.MapRoute("Arb1List","Produits/Arb1/List",new { controller = "Produits", action = "Arb1List" });

            routes.MapRoute("Validate", "Produits/Validate", new { controller = "Produits", action = "Validate" });

            routes.MapRoute("ChangeOccured", "Produits/ChangeOccured", new { controller = "Produits", action = "ChangeOccured" });

            /* ------------------------- */




            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
