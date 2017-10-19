﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PlainConcepts.WebDay;
using PlainConcepts.WebDay.Infrastructure.Authentication.Policies;
using PlainConcepts.WebDay.Infrastructure.Authentication.Requirements;
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
                    options.AddPolicy(Policy.Admin, policy => policy.AddRequirements(new UserLevelRequirement(Policy.Admin)));
                    options.AddPolicy(Policy.Writer, policy => policy.AddRequirements(new UserLevelRequirement(Policy.Writer)));
                })
                .AddDataAnnotations()                
                .AddJsonFormatters(setup =>
                {
                    setup.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }).AddFluentValidation(v => v.RegisterValidatorsFromAssembly(typeof(ApiConfiguration).Assembly));
            
            return services;
        }
    }
}
