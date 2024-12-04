
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace KalkulatorWalutowyWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.RegisterDatabase();
            builder.Services.AddDbContext<KalkulatorContext>(options =>
                options.UseMySql("server=127.0.0.1;port=3306;database=kalkulator;user=root;password=secret",
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql")));

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
