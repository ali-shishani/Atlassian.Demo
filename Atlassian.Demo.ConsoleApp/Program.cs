using Atlassian.Demo.Config.Provider;
using Atlassian.Demo.Services;
using Atlassian.Demo.Services.ConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Atlassian.Demo.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using ILoggerFactory loggerFactory =
                LoggerFactory.Create(builder =>
                    builder.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss ";
                    }));

                ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
                using (logger.BeginScope("[scope is enabled]"))
                {
                    var serviceCollection = new ServiceCollection();
                    var configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                            .Build();

                    serviceCollection.AddSingleton<IConfiguration>(configuration);

                    serviceCollection
                    .AddLogging()
                    .AddOptions();

                    serviceCollection.Configure<AppConfigurationProvider>(configuration);

                    var startup = new Startup(configuration, null, false);
                    startup.ConfigureServices(serviceCollection);

                    var serviceProvider = serviceCollection.BuildServiceProvider();
                    var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                    using var scope = serviceScopeFactory.CreateScope();

                    logger.LogInformation("Console app is running");

                    // run the console app service to implement DI.
                    var consoleApp = scope.ServiceProvider.GetRequiredService<IConsoleAppService>();
                    consoleApp.RunConsole();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("---- customised handling of the errors----");
                Console.WriteLine($"Console app errors are handled here: {ex.Message}");
                throw;
            }
        }

    }
}
