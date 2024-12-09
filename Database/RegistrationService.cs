using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class RegistrationService
    {
        public static void RegisterDatabase(this IServiceCollection services)
        {
            string connectionString = "server=127.0.0.1;port=3306;database=kalkulator;user=root;password=secret";
            services.AddDbContextPool<KalkulatorContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IKalkulatorContext, KalkulatorContext>();
        }
    }
}
