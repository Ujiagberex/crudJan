
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using WebApiClass.Data;
using WebApiClass.DTO;
using WebApiClass.IServices;
using WebApiClass.Model;
using WebApiClass.Services;

namespace WebApiClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel(options => options.ListenAnyIP(0)); // Listen on all IPs
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>

            {
                opt.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Description = "Please follow this format. Bearer space token in double literal",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                opt.OperationFilter<SecurityRequirementsOperationFilter>();

                opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                 {
                {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
                }
             });
            });
            
            //Configuration of Automapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //Database configuration
            string connectionString = builder.Configuration.GetConnectionString("Connection");
            builder.Services.AddDbContext<StudentDbContext>(options =>
            options.UseNpgsql(connectionString));
            //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            //Configure Services
            builder.Services.AddScoped<IStudent, StudentService>();
            builder.Services.AddScoped<IAuth, AuthServices>();
            builder.Services.AddTransient<INumberCheckService, NumberCheckService>();

            //Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<StudentDbContext>().AddSignInManager().AddRoles<IdentityRole>();

            //Configuration of JWT\
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
                };
            });

            builder.Services.AddHttpClient("Number-Check", option =>
            {
                option.BaseAddress = new Uri("https://api.apilayer.com/");
                option.DefaultRequestHeaders.Add("apikey", builder.Configuration["ApilayerKey"]);
            });
            var app = builder.Build();

            // Enable Swagger for both Local and Production
            app.UseSwagger();
            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }



            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
