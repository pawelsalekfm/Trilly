using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using Trilly.ViewModels.Product;
using Trilly.ViewModels.Product.Queries;

namespace CatalogApi.Services
{
    public interface IProductQueryService
    {
        Task<ProductQueryResponse> GetProductsByName(string name);
    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly CatalogContext _context;
        private readonly IMapper _mapper;

        public ProductQueryService(CatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductQueryResponse> GetProductsByName(string name)
        {
            var response = new ProductQueryResponse();

            var products = await _context.Products.AsNoTracking().Where(c => c.Name.Contains(name)).ToListAsync();

            foreach (var product in products)
            {
                var productvm = _mapper.Map<Product, ProductVM>(product);

                if (productvm.IsCorrect())
                    response.Products.Add(productvm);
            }

            response.Counter = response.Products.Count;
            response.MinPrice = response.Products.Min(c => c.PricePromotion);
            response.MaxPrice = response.Products.Max(c => c.PricePromotion);

            return response;
        }
    }
}
