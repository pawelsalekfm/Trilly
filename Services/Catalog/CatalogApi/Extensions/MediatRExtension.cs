using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Application.Category.GetMainCategories;
using CatalogApi.Application.Category.GetSubcategory;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApi.Extensions
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetMainCategoriesHandler));
            services.AddMediatR(typeof(GetSubcategoryHandler));




            return services;
        }
    }
}
