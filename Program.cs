
using Microsoft.EntityFrameworkCore;
using WebApiClass.Data;
using WebApiClass.DTO;
using WebApiClass.IServices;
using WebApiClass.Services;

namespace WebApiClass
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
            
            //Configuration of Automapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //Database configuration
            string connectionString = builder.Configuration.GetConnectionString("Connection");
            builder.Services.AddDbContext<StudentDbContext>(options =>
            options.UseNpgsql(connectionString));
            //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            //Configure Services
            builder.Services.AddScoped<IStudent, StudentService>();

            var app = builder.Build();

            // Enable Swagger for both Local and Production
            app.UseSwagger();
            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

           

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
