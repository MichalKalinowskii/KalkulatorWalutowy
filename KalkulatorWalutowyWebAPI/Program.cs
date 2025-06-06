
using Database;
using Microsoft.EntityFrameworkCore;
using NBP;
using NBP.NBPQueries.Interfaces;
using NBP.NBPQueries;
using User;

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

            builder.Services.RegisterDatabase();
            builder.Services.AddCors();
            builder.Services.NBPServicesRegistrationExtension();
            builder.Services.UserServicesRegistrationExtension();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(corsBulider => corsBulider.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
