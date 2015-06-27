using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyLogging
{
    using Microsoft.Owin;
    using Owin;
    // use an alias for the OWIN AppFunc:
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class SillyLogging
    {
        AppFunc _next;
        public SillyLogging(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            // Pass everything up through the pipeline first:
            await _next.Invoke(environment);

            // Do the logging on the way out:
            IOwinContext context = new OwinContext(environment);
            Console.WriteLine("URI: {0} Status Code: {1}", context.Request.Uri, context.Response.StatusCode);
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseSillyLogging(this IAppBuilder app)
        {
            app.Use<SillyLogging>();
        }
    }
}
