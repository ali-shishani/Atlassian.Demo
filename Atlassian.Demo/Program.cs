
using Microsoft.EntityFrameworkCore;
using Atlassian.Demo.Config.Provider;
using Atlassian.Demo.Data.Entity;
using Microsoft.Extensions.Logging;
using Atlassian.Demo.Services;

namespace Atlassian.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var hostEnv = builder.Environment;
            var configuration = builder.Configuration;

            configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var startup = new Startup(configuration, hostEnv);
            startup.ConfigureServices(builder.Services);
            var app = builder.Build();

            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Environment: {Env}", hostEnv.EnvironmentName);

            // if needed, you can seed the database like this
            //using (IServiceScope scope = app.Services.CreateScope())
            //{
            //    IServiceProvider services = scope.ServiceProvider;

            //    try
            //    {
            //        logger.LogInformation("Start Seeding Data");
            //        SeedData.Seed(services, hostEnv.EnvironmentName);
            //        logger.LogInformation("Seeding of Data Completed");
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError(ex, "Failed database seed - {Message}", ex.GetBaseException().Message);
            //        throw;
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseStaticFiles();
            app.UseRouting();

            // original code
            app.UseHttpsRedirection();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
