
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;
using Net_6_Assignment.Config;
using Net_6_Assignment.Data;
using Net_6_Assignment.Init;
using Net_6_Assignment.Models;
using Net_6_Assignment.Service;
using Net_6_Assignment.Services;
using Net_6_Assignment.Utilities.Filters;


namespace Net_6_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromFile("nLog.config").GetCurrentClassLogger();
            var policyName = "defalutPolicy";

            var builder = WebApplication.CreateBuilder(args);

            #region filter

            builder.Services.AddControllers(option =>
            {
                //global filter register, working for all actions
                option.Filters.Add<ModelValidationFilter>();
                option.Filters.Add<CommonResultFilter>();
                option.Filters.Add<ExceptionFilter>();
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            #endregion

            #region service instances
            // auto mapper instance
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // customized instances
            builder.Services.AddScoped<IService<User>, UserService>();
            builder.Services.AddTransient<IService<Category>, CategoryService>();
            builder.Services.AddTransient<IService<Course>, CourseService>();

            #endregion

            #region DB connection
            // dependency injection
            builder.Services.Configure<DBConnectionConfig>(builder.Configuration);

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));//database version

            var connectionString = builder.Configuration["DBConnection"];

            builder.Services.AddDbContext<A6DbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            #endregion

            // disable auto model validation
            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            #region JWT
            // get jwt config values from appsettings and create an obj using JWTConfig model class, IOC will handle dependency injection
            builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection(JWTConfig.Section));
            // directly get jwt config value from appsettings and construct into an obj
            var jwtConfig = builder.Configuration.GetSection(JWTConfig.Section).Get<JWTConfig>();

            builder.Services.AddJWTEXT(jwtConfig);

            builder.Services.AddTransient<CreateTokenService>();

            #endregion

            #region cors
            // cors
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(policyName, policy =>
                {

                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            #endregion

            // swagger config => see more details in swagger config extension
            builder.Services.AddSwaggerEXT();

            #region NLog
            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            #endregion

            // middleware
            var app = builder.Build();

            app.UseCors(policyName);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerEXT();
            }

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
