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

        // Add a member to hold the greeting:
        string _greeting;

        public MyMiddlewareComponent(AppFunc next, string greeting)
        {
            _next = next;
            _greeting = greeting;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            // Insert the _greeting into the display text:
            await context.Response.WriteAsync(string.Format("<h1>{0}</h1>", _greeting));
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

    public class MyMiddlewareConfigOptions
    {
        string _greetingTextFormat = "{0} from {1}{2}";
        public MyMiddlewareConfigOptions(string greeting, string greeter)
        {
            GreetingText = greeting;
            Greeter = greeter;
            Date = DateTime.Now;
        }

        public string GreetingText { get; set; }
        public string Greeter { get; set; }
        public DateTime Date { get; set; }

        public bool IncludeDate { get; set; }

        public string GetGreeting()
        {
            string DateText = "";
            if (IncludeDate)
            {
                DateText = string.Format(" on {0}", Date.ToShortDateString());
            }
            return string.Format(_greetingTextFormat, GreetingText, Greeter, DateText);
        }
    }
}
