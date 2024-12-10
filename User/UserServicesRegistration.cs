using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.UserCommands.Interfaces;
using User.UserQueries.Interfaces;

namespace User
{
    public static class UserServicesRegistration
    {
        public static void UserServicesRegistrationExtension(this IServiceCollection services)
        {
            services.AddScoped<IUserQueries, UserQueries.UserQueries>();
            services.AddScoped<IUserCommands, UserCommands.UserCommands>();
        }
    }
}
