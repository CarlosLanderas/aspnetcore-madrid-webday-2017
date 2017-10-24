using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlainConcepts.WebDay;
using PlainConcepts.WebDay.Application.Queries;
using PlainConcepts.WebDay.Infrastructure.Authentication;
using PlainConcepts.WebDay.Infrastructure.Authentication.Policies;
using PlainConcepts.WebDay.Infrastructure.Authentication.Requirements;
using PlainConcepts.WebDay.Infrastructure.Binders;
using PlainConcepts.WebDay.Infrastructure.filters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcCoreDefault(this IServiceCollection services)
        {
            services
                .AddMvcCore(options => {
                    options.Filters.Add(new ModelStateValidatorFilter());
                })
                .AddAuthorization(options =>
                {
                    options.AddPolicy(Actions.CreateUser, policy => policy.AddRequirements(
                        new UserLevelRequirement(PolicyGroup.CreateUserGroupPolicy)
                    ));
                    options.AddPolicy(Actions.ListUsers, policy => policy.AddRequirements(
                        new UserLevelRequirement(PolicyGroup.ListUsersGroupPolicy)
                    ));

                    options.AddPolicy(Actions.RemoveUser, policy => policy.AddRequirements(
                        new UserLevelRequirement(PolicyGroup.RemoveUserGroupPolicy)
                    ));
                })
                .AddDataAnnotations()                
                .AddJsonFormatters(setup =>
                {
                    setup.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }).AddFluentValidation(v => v.RegisterValidatorsFromAssembly(typeof(ApiConfiguration).Assembly));
            
            return services;
        }

        public static void AddApplicationQueries(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(c =>
                new SqlConnection(configuration.GetSection("ConnectionString").Value));

            services.AddTransient<GetUsersQuery>();
        }
    }
}
