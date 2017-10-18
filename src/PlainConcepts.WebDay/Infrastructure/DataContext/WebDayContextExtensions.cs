using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.Infrastructure.DataContext
{
    public static class WebDayContextExtensions
    {
        public static void Seed(this WebDayDbContext dbContext)
        {
            if (!dbContext.UserRoles.Any())
            {
                dbContext.Roles.Add(Role.Admin);
                dbContext.Roles.Add(Role.Writer);
                dbContext.Roles.Add(Role.Reader);

                dbContext.SaveChanges();
            }
        }
    }
}
