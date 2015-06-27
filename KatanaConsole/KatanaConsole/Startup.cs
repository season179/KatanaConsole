using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace KatanaConsole
{
    using System.IO;
    using Microsoft.Owin;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Set up the configuration options:
            var options = new MyMiddlewareConfigOptions("Greetings!", "John");
            options.IncludeDate = true;

            // Pass options along in call to extension method:
            app.UseMyMiddleware(options);
            app.UseMyOtherMiddleware();
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseMyMiddleware(this IAppBuilder app,
        MyMiddlewareConfigOptions configOptions)
        {
            app.Use<MyMiddlewareComponent>(configOptions.GetGreeting());
        }

        public static void UseMyOtherMiddleware(this IAppBuilder app)
        {
            app.Use<MyOtherMiddlewareComponent>();
        }
    }
}
