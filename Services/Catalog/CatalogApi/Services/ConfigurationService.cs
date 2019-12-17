using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CatalogApi.Services
{
    public interface IConfigurationService
    {
        string GetSqlServerConnectionString();
    }

    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSqlServerConnectionString()
        {
            try
            {
                var server = _configuration["DatabaseServer"];
                var database = _configuration["DatabaseName"];
                var user = _configuration["DatabaseUser"];
                var password = _configuration["DatabasePassword"];
                var port = _configuration["DataBasePort"];

                return $"Server={server};Database={database};User={user};Password={password};MultipleActiveResultSets=true;";


            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
