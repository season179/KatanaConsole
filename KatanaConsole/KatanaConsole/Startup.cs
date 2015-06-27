using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.IO;
using Microsoft.Owin;
using SillyLogging;
using SillyAuthentication;
using MyMidddleware;

namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseSillyLogging();
            app.UseSillyAuthentication();

            // Set up the configuration options:
            var options = new MyMiddlewareConfigOptions("Greetings!", "John");
            options.IncludeDate = true;

            // Pass options along in call to extension method:
            app.UseMyMiddleware(options);
            //app.UseMyOtherMiddleware();
        }
    }
}
