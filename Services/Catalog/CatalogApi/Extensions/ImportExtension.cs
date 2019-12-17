using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CatalogApi.Models;
using CatalogApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trilly.ViewModels.Category;
using Trilly.ViewModels.Product;

namespace CatalogApi.Extensions
{
    public static class ImportExtension
    {
        public static void ImportCategoriesFromMultiTraderJson(ICategoryImportService service)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            try
            {
                var jsonString = File.ReadAllText("Files/MultiTraderDb_dbo_Category.json");
                var jsonModel = JsonSerializer.Deserialize<List<JsonCategoryImportVM>>(jsonString);

                service.AddCategoryList(jsonModel).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ImportProductsFromMultiTraderJson(IProductImportService service)
        {
            try
            {
                var jsonString = File.ReadAllText("Files/MultiTraderDb_dbo_Product.json");
                var jsonModel = JsonSerializer.Deserialize<List<JsonProductImportVM>>(jsonString);

                service.AddProductsList(jsonModel).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ImportProductAttributesFromMultiTraderJson(IProductImportService service)
        {
            try
            {
                var jsonString = File.ReadAllText("Files/MultiTraderDb_dbo_Product.json");
                var jsonModel = JsonSerializer.Deserialize<List<JsonProductImportVM>>(jsonString);

                Console.WriteLine($"Ilosc produktów: {jsonModel.Count}");

                var jsonString2 = File.ReadAllText("Files/MultiTraderDb_dbo_ProductAttribute.json");
                var jsonModel2 = JsonSerializer.Deserialize<List<JsonProductAttributeImportVm>>(jsonString2);
                Console.WriteLine($"Ilosc atrybutów: {jsonModel2.Count}");


                service.AddProductAttributesList(jsonModel, jsonModel2).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ImportProductPhotosFromMultiTraderJson(IProductImportService service)
        {
            try
            {
                var jsonString = File.ReadAllText("Files/MultiTraderDb_dbo_Product.json");
                var jsonModel = JsonSerializer.Deserialize<List<JsonProductImportVM>>(jsonString);

                Console.WriteLine($"Ilosc produktów: {jsonModel.Count}");

                var jsonString2 = File.ReadAllText("Files/MultiTraderDb_dbo_ProductPhoto.json");
                var jsonModel2 = JsonSerializer.Deserialize<List<JsonProductPhotoImportVM>>(jsonString2);

                var jsonString3 = File.ReadAllText("Files/MultiTraderDb_dbo_Photo.json");
                var jsonModel3= JsonSerializer.Deserialize<List<JsonPhotoImportVM>>(jsonString3);

                service.AddProductPhotosList(jsonModel, jsonModel2, jsonModel3).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
