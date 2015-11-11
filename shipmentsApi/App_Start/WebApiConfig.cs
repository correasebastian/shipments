using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace shipmentsApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            // pascal to camelcase  http://stackoverflow.com/questions/28552567/web-api-2-how-to-return-json-with-camelcased-property-names-on-objects-and-the
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter .SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver();

        }
    }
}