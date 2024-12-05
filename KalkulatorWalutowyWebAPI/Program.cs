
using Database;
using Microsoft.EntityFrameworkCore;
using NBP;

namespace KalkulatorWalutowyWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.RegisterDatabase();

            builder.Services.AddDbContextPool<KalkulatorContext>(options =>
                options.UseMySql("server=127.0.0.1;port=3306;database=kalkulator;user=root;password=secret",
                    ServerVersion.AutoDetect("server=127.0.0.1;port=3306;database=kalkulator;user=root;password=secret")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.NBPServicesRegistrationExtension();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
