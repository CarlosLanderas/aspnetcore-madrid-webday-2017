using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;
using PlainConcepts.WebDay;
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
                .AddDataAnnotations()                
                .AddJsonFormatters(setup =>
                {
                    setup.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }).AddFluentValidation(v => v.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

            return services;
        }
    }
}
