using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using shipmentsApi;
using shipmentsApi.App_Start;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Policy;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Startup))]

namespace shipmentsApi
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureAuthZero(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }


    }

    public partial class Startup
    {
        private void ConfigureAuthZero(IAppBuilder app)
        {
            var issuer = "https://" + ConfigurationManager.AppSettings["auth0:Domain"] + "/";
            var audience = ConfigurationManager.AppSettings["auth0:ClientId"];
            var secret = TextEncodings.Base64.Encode(
                TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["auth0:ClientSecret"]));

            //ApplicationSecurityInfo SE GENER AUN TOOKEN CON LA IMPLEMENTACION DE FIREBASE
            var tokenGenerator = new Firebase.TokenGenerator(ConfigurationManager.AppSettings["firebase:Secret"]);

            var authPayload = new Dictionary<string, object>()
                {
                  { "uid", "1" },
                  { "some", "arbitrary" },
                  { "data", "here" }
                };
            string token = tokenGenerator.CreateToken(authPayload, new Firebase.TokenOptions(admin: true));

            //ApplicationSecurityInfo SE VALIDA UN TOKEN SI FURE CREADO CON NUESTRO FIREBASE SECRET,PERO COMO AGREGAR ESTO AL PIPELINE PARA AUTHORIZAR METODOS
            try
            {
                string jsonPayload = JWT.JsonWebToken.Decode(token, ConfigurationManager.AppSettings["firebase:Secret"]);
                Console.WriteLine(jsonPayload);
            }
            catch (JWT.SignatureVerificationException)
            {
                Console.WriteLine("Invalid token!");
            }

            var jwtOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audience },
                IssuerSecurityTokenProviders = new[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                }
            };

            app.UseJwtBearerAuthentication(jwtOptions);

        }
    }
}