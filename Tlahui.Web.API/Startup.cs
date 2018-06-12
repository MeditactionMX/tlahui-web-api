using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using SimpleInjector.Lifestyles;

namespace Tlahui.Web.API
{
    public partial class Startup
    {
        // The OWIN middleware will invoke this method when the app starts
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
