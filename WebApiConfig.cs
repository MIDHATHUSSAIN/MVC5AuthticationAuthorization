using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApiThrottle;

namespace webAPIandMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perMinute: 5) // 5 requests per minute
                {
                    IpThrottling = true
                },
                Repository = new MemoryCacheRepository()
            });

          config.MapHttpAttributeRoutes();
        
            //config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //    config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
     
    }
}
