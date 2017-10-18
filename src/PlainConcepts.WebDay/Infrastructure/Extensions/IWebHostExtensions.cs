using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlainConcepts.WebDay.Infrastructure.DataContext;

namespace PlainConcepts.WebDay.Infrastructure.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost Migrate<TContext>(this IWebHost webHost, Action<TContext> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<TContext>();
                    context.Database.Migrate();
                    seeder(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return webHost;
        }
    }
}
