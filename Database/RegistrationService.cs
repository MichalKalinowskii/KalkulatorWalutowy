using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class RegistrationService
    {
        public static void RegisterDatabase(IServiceCollection services)
        {
            services.AddDbContext<KalkulatorContext>(options =>
                options.UseMySql("server=127.0.0.1;port=3306;database=kalkulator;user=root;password=secret",
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql")));
        }
    }
}
