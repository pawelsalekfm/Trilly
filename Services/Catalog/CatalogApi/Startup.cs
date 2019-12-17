using CatalogApi.Extensions;
using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using CatalogApi.Models.Automapper;
using CatalogApi.Services.Grpc;
using Microsoft.AspNetCore.Http;

namespace CatalogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddGrpc();

            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaxReceiveMessageSize = 2 * 1024 * 1024;
                options.MaxSendMessageSize = 5 * 1024 * 1024;
            });

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllers();
            services.AddCustomDbContext(Configuration);
            services.AddMediatRConfiguration();


            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryDapperQueryService, CategoryDapperQueryService>();
            services.AddTransient<ICategoryImportService, CategoryImportService>();
            services.AddTransient<IProductImportService, ProductImportService>();
            services.AddTransient<IProductQueryService, ProductQueryService>();
            services.AddTransient<IProductDapperQueryService, ProductDapperQueryService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CategoryGrpcService>();
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapControllers();


                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<CatalogContext>();
                context.MigrateDB();

                var enableImport = Configuration["ImportDemoSeed"];

                if (enableImport == "1")
                {
                    // zablokowane z powodu brak zgody na publikacjê bazy produktów.
//                    var importCategoryService = scope.ServiceProvider.GetService<ICategoryImportService>();
//                    ImportExtension.ImportCategoriesFromMultiTraderJson(importCategoryService);
//
//                    var importProductService = scope.ServiceProvider.GetService<IProductImportService>();
//                    ImportExtension.ImportProductsFromMultiTraderJson(importProductService);
//                    ImportExtension.ImportProductAttributesFromMultiTraderJson(importProductService);
//                    ImportExtension.ImportProductPhotosFromMultiTraderJson(importProductService);
                }
            }
        }
    }
}
