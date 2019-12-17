using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PromotorApi.Services;

namespace PromotorApi.Extensions
{
    public static class ApplicationsServicesConfiguration
    {
        public static IServiceCollection AddApplicationServicesConfiguration(this IServiceCollection services)
        {
            services.AddTransient<ISlideShowService, SlideShowService>();


            return services;
        }
    }
}
