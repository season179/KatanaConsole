using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace KatanaConsole
{
    using System.IO;
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
                    // Do something with the incoming request:
                    var response = environment["owin.ResponseBody"] as Stream;
                    using (var writer = new StreamWriter(response))
                    {
                        await writer.WriteAsync("<h1>Hello from my first middleware</h1>");
                    }
                    // Call the next Middleware in the chain:
                    await next.Invoke(environment);
                };
            return appFunc;
        }

        public AppFunc MyOtherMiddleware(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
                {
                    // Do something with the incoming request:
                    var response = environment["owin.ResponseBody"] as Stream;
                    using (var writer = new StreamWriter(response))
                    {
                        await writer.WriteAsync("<h1>Hello from my second middleware</h1>");
                    }
                    // Call the next Middleware in the chain:
                    await next.Invoke(environment);
                };
            return appFunc;
        }
    }
}
