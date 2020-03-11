using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebAPI.Custom;

namespace EmployeeService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // CONVENSIONAL ROUTING
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // WEB API VERSIONING USING QUERYSTRING, CUSTOM HEADER, ACCEPT HEADER, MEDIA TYPE
            //config.Services.Replace(typeof(IHttpControllerSelector),
            //    new CustomControllerSelector(config));

            // WEB API VERSIONING USING CUSTOM MEDIA TYPE IN ACCEPT HEADER
            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //    .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/mmw.manish.student.v1+json"));
            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //    .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/mmw.manish.student.v2+json"));

            //config.Formatters.XmlFormatter.SupportedMediaTypes
            //    .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/mmw.manish.student.v1+xml"));
            //config.Formatters.XmlFormatter.SupportedMediaTypes
            //    .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/mmw.manish.student.v2+xml"));

            // WEB API VERSIONING USING URIS
            //config.Routes.MapHttpRoute(
            //    name: "Version1",
            //    routeTemplate: "api/student/{id}",
            //    defaults: new { id = RouteParameter.Optional, Controllers = "StudentV1" }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "Version2",
            //    routeTemplate: "api/student/{id}",
            //    defaults: new { id = RouteParameter.Optional, Controllers = "StudentV2" }
            //);
        }
    }
}
