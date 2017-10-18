using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.Infrastructure.DataContext
{
    public class WebDayDbContext : DbContext
    {
        public WebDayDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userEntity = modelBuilder.Entity<User>();
            userEntity.HasKey(u => u.Id);
            userEntity.Property(u => u.Id)
                .IsRequired();


        }
    }
}
