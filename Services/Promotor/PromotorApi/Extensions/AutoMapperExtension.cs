using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PromotorApi.Mappings;

namespace PromotorApi.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SlideShowMappingProfile));
            services.AddAutoMapper(typeof(SlideShowToVmMappingProfile));
            services.AddAutoMapper(typeof(SlideShowItemToVmMappingProfile));
            services.AddAutoMapper(typeof(SlideShowItemToVmMappingProfile));
            
            return services;
        }
    }
}
