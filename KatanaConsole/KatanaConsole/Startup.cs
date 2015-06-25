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
            var middleware = new Func<AppFunc, AppFunc>(MyMiddleware);
            var otherMiddleware = new Func<AppFunc, AppFunc>(MyOtherMiddleware);

            app.Use(middleware);
            app.Use(otherMiddleware);
        }

        public AppFunc MyMiddleware(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
                {
                    IOwinContext context = new OwinContext(environment);
                    await context.Response.WriteAsync("<h1>Hello from My First Middleware</h1>");
                    await next.Invoke(environment);
                };
            return appFunc;
        }

        public AppFunc MyOtherMiddleware(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
                {
                    IOwinContext context = new OwinContext(environment);
                    await context.Response.WriteAsync("<h1>Hello from My Other Middleware</h1>");
                    await next.Invoke(environment);
                };
            return appFunc;
        }
    }
}
