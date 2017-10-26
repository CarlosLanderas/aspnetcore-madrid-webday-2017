using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlainConcepts.WebDay.Infrastructure.DataContext;
using PlainConcepts.WebDay.Infrastructure.Extensions;

namespace PlainConcepts.WebDay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .Migrate<WebDayDbContext>(c => c.Seed())
                .Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<ApiConfiguration>()
                .Build();
    }
    
}
