using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Trilly.Tools;
using Trilly.ViewModels.Product;
using Trilly.ViewModels.Product.Queries;

namespace CatalogApi.Services
{
    public interface IProductDapperQueryService
    {
        Task<ProductQueryResponse> GetProductsByName(string name);
    }

    public class ProductDapperQueryService : IProductDapperQueryService
    {
        private readonly IMapper _mapper;

        public ProductDapperQueryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ProductQueryResponse> GetProductsByName(string name)
        {
            var sqlCommand = $"select * from CatalogApi_Product where Name like '%{name}%';";
            var response = new ProductQueryResponse();

            using (var connection = new SqlConnection(StaticSettingsTool.GetTrilloDbAddressForTest()))
            {
                connection.Open();

                var result = await connection.QueryAsync<Product>(sqlCommand);

                if (!result.AsList().Any())
                    return response;

                foreach (var product in result)
                {
                    var productvm = _mapper.Map<Product, ProductVM>(product);

                    if (productvm.IsCorrect())
                        response.Products.Add(productvm);
                }
            }

            if (response.Products.Any())
            {
                response.Counter = response.Products.Count;
                response.MinPrice = response.Products.Min(c => c.PricePromotion);
                response.MaxPrice = response.Products.Max(c => c.PricePromotion);
            }

            return response;
        }
    }
}
