using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Web.GatewayApi.Services;

namespace Web.GatewayApi.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddGatewayCollectionServices(this IServiceCollection services)
        {
            services.AddHttpClient<ICatalogService, CatalogService>();

            //services.AddTransient<ICatalogService, CatalogService>();

            return services;
        }
    }
}
