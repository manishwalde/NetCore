
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPI.Custom
{
    // Derive from the DefaultHttpControllerSelector class
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor
            SelectController(HttpRequestMessage request)
        {
            // Get all the available Web API controllers
            var controllers = GetControllerMapping();
            // Get the controller name and parameter values from the request URI
            var routeData = request.GetRouteData();

            // Get the controller name from route data.
            // The name of the controller in our case is "Students"
            var controllerName = routeData.Values["controller"].ToString();

            // Default version number to 1
            string versionNumber = "1";

            // WEB API VERSIONING USING QUERYSTRING
            var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            if (versionQueryString["v"] != null)
            {
                versionNumber = versionQueryString["v"];
            }

            /* WEB API VERSIONING USING CUSTOM HEADER
            // Get the version number from Custom version header
            // This custom header can have any name. We have to use this
            // same header to specify the version when issuing a request
            // X-StudentService-Version: 1
             */
            //string customHeader = "X-StudentService-Version";
            //if (request.Headers.Contains(customHeader))
            //{
            //    versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();

            //    if(versionNumber.Contains(','))
            //    {
            //        versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(','));
            //    }
            //}

            /* WEB API VERSIONING USING ACCEPT HEADER IN REQUEST
            // Accept: application/json; version=1
            */
            //var acceprHeader = request.Headers.Accept
            //    .Where(a => a.Parameters.Count(p => p.Name.ToLower() == "version") > 0);
            //if(acceprHeader.Any())
            //{
            //    versionNumber = acceprHeader.First().Parameters.First(p => p.Name.ToLower() == "version").Value;
            //}


            /* WEB API VERSIONING USING CUSTOM MEDIA TYPE IN ACCEPT HEADER
            // Accept: application/mmw.manish.student.v1+json
            // application\/mmw\.manish\.([a-z]+)\.v([0-9]+)\+([a-z]+)  (Added escape sequences and converted to RegEx)
            */
            //var regex = @"application\/mmw\.manish\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";

            //var acceprHeader = request.Headers.Accept
            //    .Where(a => Regex.IsMatch(a.MediaType, regex, RegexOptions.IgnoreCase));
            //if (acceprHeader.Any())
            //{
            //    var match = Regex.Match(acceprHeader.First().MediaType, regex, RegexOptions.IgnoreCase);
            //    versionNumber = match.Groups["version"].Value;
            //}

            if (versionNumber == "1")
            {
                // if version number is 1, then append V1 to the controller name.
                // So at this point the, controller name will become StudentsV1
                controllerName = controllerName + "V1";
            }
            else
            {
                // if version number is 2, then append V2 to the controller name.
                // So at this point the, controller name will become StudentsV2
                controllerName = controllerName + "V2";
            }

            HttpControllerDescriptor controllerDescriptor;
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            return null;
        }
    }
}