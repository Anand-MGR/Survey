using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(YourNamespace.Startup))]

namespace BMWC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IdentityModelEventSource.ShowPII = true;
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}