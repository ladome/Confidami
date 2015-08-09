using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Confidami.Web.Helpers
{
    public static class MvcHelper
    {
        public static string GetRouteName(this Route route)
        {
            if (route == null)
            {
                return null;
            }
            return route.DataTokens.GetRouteName();
        }

        public static string GetRouteName(this RouteData routeData)
        {
            if (routeData == null)
            {
                return null;
            }
            return routeData.DataTokens.GetRouteName();
        }

        public static string GetRouteName(this RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                return null;
            }
            object routeName = null;
            routeValues.TryGetValue("__RouteName", out routeName);
            return routeName as string;
        }

        public static Route SetRouteName(this Route route, string routeName)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }
            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }
            route.DataTokens["__RouteName"] = routeName;
            return route;
        }
    }
}