using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            var server = configuration["DatabaseServer"];
            var database = configuration["DatabaseName"];
            var user = configuration["DatabaseUser"];
            var password = configuration["DatabasePassword"];
            var port = configuration["DataBasePort"];

            var connectionString = $"Server={server};Database={database};User={user};Password={password};MultipleActiveResultSets=true;";
            services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));

            return services;
        }
    }
}
