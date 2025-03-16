using Atlassian.Demo.Config.Provider;
using Atlassian.Demo.Data;
using Atlassian.Demo.Repositories;
using Atlassian.Demo.Services.ConsoleApp;
using Atlassian.Demo.Services.Mood;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Atlassian.Demo.Services
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger<Startup> _logger;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly bool _isWeb;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment, bool isWeb = true)
        {
            Configuration = configuration;
            _hostEnv = hostEnvironment;

            var loggerFactory = LoggerFactory.Create(builder =>
                builder.AddSimpleConsole(options => options.SingleLine = true));
            _logger = loggerFactory.CreateLogger<Startup>();
            _isWeb = isWeb;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddSingleton<IAppConfigurationProvider, AppConfigurationProvider>();
            services.AddScoped<AtlassianDemoDbContext>();
            services.AddDbContext<AtlassianDemoDbContext>();
            services.AddHttpClient();

            services.AddCors(o => o.AddDefaultPolicy(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()));

            RegisterRepositories(services);
            RegisterServices(services);

            if (_isWeb)
            {
                services.AddControllers();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            }
        }

        private IServiceCollection RegisterServices(IServiceCollection services)
        {
            // register services
            services.AddScoped<IConsoleAppService, ConsoleAppService>();
            services.AddTransient<IMoodRatingService, MoodRatingService>();

            return services;
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            // register repositories
            services.AddScoped<IMoodRatingRecordRepository, MoodRatingRecordRepository>();
        }
    }
}
