using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using shipmentsApi;
using shipmentsApi.App_Start;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace shipmentsApi
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}