using Atlassian.Demo.Config.Provider;
using Atlassian.Demo.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Data
{
    public class AtlassianDemoDbContext : DbContext
    {
        static string connectionString = "";
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public AtlassianDemoDbContext(IAppConfigurationProvider appConfigurationProvider) : base()
        {
            _appConfigurationProvider = appConfigurationProvider;
            connectionString = _appConfigurationProvider.GetConnectionString();
        }

        // add your entities here
        public virtual DbSet<MoodRatingRecord> MoodRatingRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString, mySqlOptions =>
            {
                // TODO: wire these options in appsettings.json
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,  // Adjust the maximum number of retry attempts as needed
                    maxRetryDelay: TimeSpan.FromSeconds(30),  // Adjust the maximum delay between retries as needed
                    errorNumbersToAdd: null);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
