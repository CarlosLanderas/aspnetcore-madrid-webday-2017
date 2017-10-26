
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PlainConcepts.WebDay.Infrastructure.MIddleware;

namespace PlainConcepts.WebDay
{
    public class Startup
    {
        //Sample middlewares live coded in talk
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IGreeter, Greeter>();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<TimeMeasureMiddleware>();
            app.Run(async context =>
            {
                var name = context.Request.Query["name"];
                var greeter = context.RequestServices.GetService<IGreeter>();
                await context.Response.WriteAsync(greeter.Salute(name));
            });
        }
    }

    public class TimeMeasureMiddleware
    {
        private readonly RequestDelegate next;

        public TimeMeasureMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await next(context);
            sw.Stop();
            Console.WriteLine($"Elapsed ticks  {sw.ElapsedTicks} ticks");
        }
    }

    public interface IGreeter
    {
        string Salute(string name);
    }

    public class Greeter : IGreeter
    {
        public string Salute(string name)
        {
            return $"Hello {name}";
        }
    }
}
