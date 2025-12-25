
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Repositories;
using QwiikTechnicalTest.Services;
using QwiikTechnicalTest.Utilities;
using QwikkTechnicalTest.Services;

namespace QwiikTechnicalTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            builder.Services.AddDbContext<ApplicationContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<NewTokenGenerator>();

            builder.Services.AddScoped<CustomerRepository>();
            builder.Services.AddScoped<AppointmentRepository>();

            builder.Services.AddScoped<ICustomer, CustomerService>();
            builder.Services.AddScoped<IAppointment, AppointmentService>();

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
