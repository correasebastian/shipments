﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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

        }
    }
}