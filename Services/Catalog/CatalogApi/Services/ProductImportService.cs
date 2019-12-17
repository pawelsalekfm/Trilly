using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogApi.Models;
using Trilly.ViewModels.Product;

namespace CatalogApi.Services
{
    public interface IProductImportService
    {
        Task AddProductsList(List<JsonProductImportVM> products);
        Task AddProductAttributesList(List<JsonProductImportVM> products, List<JsonProductAttributeImportVm> attributes);
        Task AddProductPhotosList(List<JsonProductImportVM> products, List<JsonProductPhotoImportVM> productPhotos, List<JsonPhotoImportVM> photos);
    }

    public class ProductImportService : IProductImportService
    {
        private readonly CatalogContext _context;
        private readonly IMapper _mapper;

        public ProductImportService(CatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProductsList(List<JsonProductImportVM> products)
        {
            if (!products.Any())
                return;

            try
            {
                foreach (var jsonProductImportVm in products)
                {
                    Console.WriteLine($"Import produktu: {jsonProductImportVm.XlId}:{jsonProductImportVm.Name}");
                    var product = _mapper.Map<Product>(jsonProductImportVm);

                    if (_context.Products.Any(c => c.ExternalId == product.ExternalId))
                        continue;

                    _context.Products.Add(product);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task AddProductAttributesList(List<JsonProductImportVM> products, List<JsonProductAttributeImportVm> attributes)
        {
            if (!products.Any())
                return;

            if (!attributes.Any())
                return;

            Console.WriteLine("===========================");
            Console.WriteLine("     Import Atrybutów");
            Console.WriteLine("===========================");

            var productsInDatabase = _context.Products.ToList();

            foreach (var productDb in productsInDatabase)
            {
                try
                {
                    var productsJson = products.FirstOrDefault(c => c.XlId == productDb.ExternalId);
                    if (productsJson == null)
                        continue;

                    var attributesJsonList = attributes.Where(c => c.ProductId == productsJson.Id).ToList();
                    if (!attributesJsonList.Any())
                        continue;

                    Console.WriteLine($"Import {attributesJsonList.Count} atrybutów dla produktu: {productDb.ExternalId} : {productDb.Name}");

                    foreach (var jsonProductAttributeImportVm in attributesJsonList)
                    {
                        var productAttribute = _mapper.Map<ProductAttribute>(jsonProductAttributeImportVm);
                        productAttribute.ProductId = productDb.Id;

                        _context.ProductAttributes.Add(productAttribute);
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            _context.SaveChanges();

            Console.WriteLine("===========================");
            Console.WriteLine(" Koniec Importu Atrybutów");
            Console.WriteLine("===========================");
        }

        public async Task AddProductPhotosList(List<JsonProductImportVM> products,
            List<JsonProductPhotoImportVM> productPhotos,
            List<JsonPhotoImportVM> photos)
        {
            if (!products.Any())
                return;

            if (!productPhotos.Any())
                return;

            if (!photos.Any())
                return;

            var productsInDatabase = _context.Products.ToList();

            foreach (var productDb in productsInDatabase)
            {
                var productsJson = products.FirstOrDefault(c => c.XlId == productDb.ExternalId);
                if (productsJson == null)
                    continue;

                var photosJsonList = productPhotos.Where(c => c.ProductId == productsJson.Id).ToList();
                if (!photosJsonList.Any())
                    continue;

                Console.WriteLine($"Import {photosJsonList.Count} zdjęć dla produktu: {productDb.ExternalId} : {productDb.Name}");

                foreach (var jsonProductPhotoImportVm in photosJsonList)
                {
                    var photo = photos.FirstOrDefault(c => c.XlId == jsonProductPhotoImportVm.PhotoId);

                    if (photo != null)
                    {
                        _context.ProductImages.Add(new ProductImage
                        {
                            ImageUrl = photo.Link,
                            ProductId = productDb.Id
                        });
                    }
                }
            }

            _context.SaveChanges();

            Console.WriteLine("===========================");
            Console.WriteLine(" Koniec Importu Zdjęć");
            Console.WriteLine("===========================");
        }
    }
}
