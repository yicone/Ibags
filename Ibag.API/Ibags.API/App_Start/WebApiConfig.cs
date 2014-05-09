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
               defaults: new { controller = "News", id = RouteParameter.Optional }//,   // !note: defaults如果不设controller, 会导致该路由规则失效
                //constraints: new { controller = "News" } // !note: constraints如果不设controller, 会导致HelpPage不可见
           );

            config.Routes.MapHttpRoute(
                name: "Token",
                routeTemplate: "api/token/{mobileNo}/{password}",
                defaults: new { controller = "Token" }
            );
            // 注册帐号不需要token
            config.Routes.MapHttpRoute(
                name: "Registration",
                routeTemplate: "api/account/{code}",
                defaults: new { controller = "Account" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post), code = @"\d{6}" }
            );
            // 发送注册短信不需要token
            config.Routes.MapHttpRoute(
                name: "PostRegisterMessage",
                routeTemplate: "api/message/",
                defaults: new { controller = "Message" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
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
