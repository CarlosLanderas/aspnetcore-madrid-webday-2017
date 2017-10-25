using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PlainConcepts.WebDay.Infrastructure.Authentication;
using PlainConcepts.WebDay.Infrastructure.Authentication.Handlers;
using PlainConcepts.WebDay.Infrastructure.DataContext;
using PlainConcepts.WebDay.Infrastructure.MIddleware;
using PlainConcepts.WebDay.Infrastructure.Repositories;

namespace PlainConcepts.WebDay
{
    public class ApiConfiguration
    {
        public ApiConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebDayDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionString").Value);
            });


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorizationHandler, UserRightsHandler>();
            services.AddApplicationQueries(Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "WebDay",
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SymmetricSecurityKey")),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });

            services.AddMvcCoreDefault();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<TimingMiddleware>();
            app.Use(async (context, next) =>
            {
                var logger = context.RequestServices.GetService<ILogger<ApiConfiguration>>();
                logger.LogDebug($"Executing {context.Request.Method} : {context.Request.Path}");
                await next();
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
