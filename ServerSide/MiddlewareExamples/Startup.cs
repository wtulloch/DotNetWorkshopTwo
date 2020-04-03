using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewareExamples
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // add Custom middleware.

            app.Use(async (context, next) =>
            {
                Console.WriteLine("A (before)");
                await next();
                Console.WriteLine("A (After)");
                await context.Response.WriteAsync("<h3>You don't say</h3>");
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("B (before)");
                await next();
                Console.WriteLine("B (after)");
               await context.Response.WriteAsync("<h2> what!!!</h2>");
            });
            app.Run(async context =>
            {
                Console.WriteLine("C");
                await context.Response.WriteAsync("<h1>this is a test</h1>");
            });
        }
    }
}
