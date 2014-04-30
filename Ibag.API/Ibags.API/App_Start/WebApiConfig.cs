using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Ibags.API.App_Start;
using System.Web.Http.Routing;
using System.Net.Http;

namespace Ibags.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new GlobalExceptionFilterAttribute());

            //Create and instance of TokenInspector setting the default inner handler
            TokenInspector tokenInspector = new TokenInspector() { InnerHandler = new HttpControllerDispatcher(config) };

            //Just exclude the users controllers from need to provide valid token, so they could authenticate
            config.Routes.MapHttpRoute(
                name: "News",
                routeTemplate: "api/news/{id}",
                defaults: new { controller = "News", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Token",
                routeTemplate: "api/token/{mobileNo}/{password}",
                defaults: new { controller = "Token" }
            );
            config.Routes.MapHttpRoute(
                name: "Registration",
                routeTemplate: "api/account/{id}",
                defaults: new { controller = "Account", id = RouteParameter.Optional },
                constraints: new { Grendal = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "Order",
                routeTemplate: "api/order/{orderNo}",
                defaults: new { controller = "Order", orderNo = RouteParameter.Optional },
                constraints: null,
                handler: tokenInspector
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: tokenInspector
            );
            //config.MessageHandlers.Add(new HTTPSGuard());
        }
    }
}
