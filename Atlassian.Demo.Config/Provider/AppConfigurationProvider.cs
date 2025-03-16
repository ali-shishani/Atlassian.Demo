using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Config.Provider
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public AppConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration["ConnectionStrings:MySQLConnectionString"];
        }
    }
}
