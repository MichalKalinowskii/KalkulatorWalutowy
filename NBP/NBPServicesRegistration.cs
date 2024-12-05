using Microsoft.Extensions.DependencyInjection;
using NBP.NBPQueries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBP
{
    public static class NBPServicesRegistration
    {
        public static IServiceCollection NBPServicesRegistrationExtension(this IServiceCollection services)
        {
            services.AddScoped<INBPQueries, NBPQueries.NBPQueries>();

            return services;
        }
    }
}
