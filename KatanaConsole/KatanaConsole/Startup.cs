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
            app.UseMyMiddleware("This is the new greeting for MyMiddleware!");
            app.UseMyOtherMiddleware();
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseMyMiddleware(this IAppBuilder app, string greetingOption)
        {
            app.Use<MyMiddlewareComponent>(greetingOption);
        }

        public static void UseMyOtherMiddleware(this IAppBuilder app)
        {
            app.Use<MyOtherMiddlewareComponent>();
        }
    }
}
