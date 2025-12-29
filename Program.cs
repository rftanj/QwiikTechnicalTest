
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Repositories;
using QwiikTechnicalTest.Services;
using QwiikTechnicalTest.Utilities;
using QwikkTechnicalTest.Services;
using System.Reflection;
using QwiikTechnicalTest.DbSeeder;
using Microsoft.OpenApi.Models;

namespace QwiikTechnicalTest
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Appointment API",
                    Version = "v1",
                    Description = "API documentation for appointment booking - Qwiik"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });


            builder.Services.AddTransient<NewTokenGenerator>();

            builder.Services.AddScoped<CustomerRepository>();
            builder.Services.AddScoped<AppointmentRepository>();
            builder.Services.AddScoped<UserRepository>();

            builder.Services.AddScoped<ICustomer, CustomerService>();
            builder.Services.AddScoped<IAppointment, AppointmentService>();
            builder.Services.AddScoped<IUser,UserService>();

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

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                await DbSeeder.DbSeeder.SeedAsync(dbContext);
            }

            app.Run();
        }
    }
}
