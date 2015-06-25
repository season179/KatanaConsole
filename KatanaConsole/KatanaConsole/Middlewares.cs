using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace KatanaConsole
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    class Middlewares
    {
    }

    public class MyMiddlewareComponent
    {
        AppFunc _next;
        public MyMiddlewareComponent(AppFunc next)
        {
            _next = next;
        }
        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            await context.Response.WriteAsync("<h1>Hello from My First Middleware</h1>");
            await _next.Invoke(environment);
        }
    }

    public class MyOtherMiddlewareComponent
    {
        AppFunc _next;
        public MyOtherMiddlewareComponent(AppFunc next)
        {
            _next = next;
        }
        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            await context.Response.WriteAsync("<h1>Hello from My Other Middleware</h1>");
            await _next.Invoke(environment);
        }
    }
}
