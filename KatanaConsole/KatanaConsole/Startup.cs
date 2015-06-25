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
            app.Use<MyMiddlewareComponent>();
            app.Use<MyOtherMiddlewareComponent>();
        }
    }
}
