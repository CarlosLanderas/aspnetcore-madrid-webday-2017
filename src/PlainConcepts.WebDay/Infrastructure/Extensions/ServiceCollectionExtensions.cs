using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcCoreDefault(this IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddDataAnnotations()
                .AddJsonFormatters(setup =>
                {
                    setup.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            return services;
        }
    }
}
