using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.OWIN;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(YourNamespace.Startup))]

namespace YourNamespace
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

           OwinTokenAcquirerFactory owinTokenAcquirerFactory = TokenAcquirerFactory.GetDefaultInstance<OwinTokenAcquirerFactory>();

            // Configure the web app.
            app.AddMicrosoftIdentityWebApp(owinTokenAcquirerFactory,
                                           updateOptions: options => { });


            // Add the services you need.
            owinTokenAcquirerFactory.Services
                 .Configure<ConfidentialClientApplicationOptions>(options =>
                 { options.RedirectUri = "https://surveyspyder.azurewebsites.net/Login/LoginPage"; })                
                .AddMicrosoftGraph()
                .AddInMemoryTokenCaches();

            //{ options.RedirectUri = "https://surveyspyder.azurewebsites.net/Login/LoginPage"; })  
            // { options.RedirectUri = "http://localhost:53598/Login/LoginPage"; })         

            owinTokenAcquirerFactory.Build();

            
        }
    }
}
