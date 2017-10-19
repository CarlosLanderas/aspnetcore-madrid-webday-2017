using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PlainConcepts.WebDay.Model;
using PlainConcepts.WebDay.Model.Shared;

namespace PlainConcepts.WebDay.Infrastructure.DataContext
{
    public class WebDayDbContext : DbContext
    {
        public WebDayDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entryChange in ChangeTracker.Entries())
            {
                if (entryChange.State == EntityState.Added || entryChange.State == EntityState.Modified)
                {
                    if (entryChange.Entity is Entity entity)
                    {
                        entity.CreatedAt = DateTime.Now;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings
                (a => a.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userEntity = modelBuilder.Entity<User>();
            userEntity.ToTable("Users");
            userEntity.HasKey(u => u.Id);
            userEntity.Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            userEntity.Property(u => u.Id)
                .ForSqlServerUseSequenceHiLo()
                .IsRequired();

            userEntity.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            userEntity.Property(u => u.Surname)
                .HasMaxLength(50)
                .IsRequired();

            userEntity.Ignore(u => u.Roles);
            
            var roleEntity = modelBuilder.Entity<Role>();
            roleEntity.ToTable("Roles");
            roleEntity.HasKey(r => r.Id);
            roleEntity.Property(r => r.Id)
                .ValueGeneratedNever()
                .IsRequired();
            roleEntity.Property(r => r.Name)
                .HasMaxLength(20)
                .IsRequired();

            var userRoleEntity = modelBuilder.Entity<UserRole>();
            userRoleEntity.ToTable("UserRoles");
            userRoleEntity.HasKey(ur => new { ur.UserId, ur.RoleId });            

            userRoleEntity.HasOne(u => u.User)
                .WithMany("_userRoles")
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            userRoleEntity.HasOne(u => u.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();
        }
    }
}
