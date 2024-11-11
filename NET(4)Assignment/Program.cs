
using Microsoft.AspNetCore.Mvc;
using NET3Assignment.Profiles;
using NET3Assignment.Utilities.Filters;

namespace NET3Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Disable automatic model validation.
            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ModelValidationFilter>();
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<CommonResultFilter>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Print environment at the moment
            System.Diagnostics.Debug.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

            var key1 = builder.Configuration["key1"];
            System.Diagnostics.Debug.WriteLine($"key1: {key1}");
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
