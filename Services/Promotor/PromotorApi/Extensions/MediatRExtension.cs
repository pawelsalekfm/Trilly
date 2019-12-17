using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PromotorApi.Application.SlideShow.Commands.CreateSlideShow;
using PromotorApi.Application.SlideShow.Commands.DeleteSlideShow;
using PromotorApi.Application.SlideShow.Commands.UpdateSlideShow;

namespace PromotorApi.Extensions
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateSlideShowHandler));
            services.AddMediatR(typeof(UpdateSlideShowHandler));
            services.AddMediatR(typeof(DeleteSlideShowHandler));

            return services;
        }
    }
}
