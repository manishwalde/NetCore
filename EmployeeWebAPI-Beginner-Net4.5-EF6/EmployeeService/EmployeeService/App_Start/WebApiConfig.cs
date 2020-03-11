using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace EmployeeService
{
    public static class WebApiConfig
    {
        public class CustomJsonFromatter : JsonMediaTypeFormatter
        {
            public CustomJsonFromatter()
            {
                this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            }
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /// BELOW TWO LINES ARE FOR CORS CROSS DOMAIN AJAX CALLS
            /// IT WILL BE APPLICABLE GLOBALLY THROUGHOUT THE APPLICATION
            /// IF YOU WANT IT TO BE ENABLE FOR SPECIFIC CONTROLLER DECORATE THAT CONTROLLER WITH EnableCorsAttribute
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();

            /// BELOW TWO LINES ARE FOR JSONP FORMATTOR FOR CROSS DOMAIN AJAX CALLS
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            /// USER CREATED RequireHttpsAttribute 
            /// OR DECORATE CONTROLLER WITH [REQUIREHTTPS] ATTRIBUTE
            //config.Filters.Add(new RequireHttpsAttribute());

            /// USER CREATED BasicAuthenticationAttribute 
            /// OR DECORATE CONTROLLER WITH [BasicAuthentication] ATTRIBUTE
            //config.Filters.Add(new BasicAuthenticationAttribute());

            /// OWN CUSTOM FORMATTOR FUNCTION
            //config.Formatters.Add(new CustomJsonFromatter());

            /// IT WILL REMOVE XML FORMATTER AND WILL ONLY SUPPORT JSON RESULT
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"))

            /// INDENT JSON DATA
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            /// CAMEL CASE INSTEAD OF PASCAL CASE
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
