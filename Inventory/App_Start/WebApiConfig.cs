﻿using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Inventory
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        { 

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));

            
        }
    }
}
