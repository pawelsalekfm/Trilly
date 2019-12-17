using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Trilly.Tools.Exceptions;
using Trilly.ViewModels.Product;

namespace CatalogApi.Services
{
    public interface ICategoryDapperQueryService
    {
        Task<List<Category>> GetMainCategories();
        Task<List<Category>> GetSubCategories(int categoryId);
    }

    public class CategoryDapperQueryService : ICategoryDapperQueryService
    {
        public readonly IConfigurationService _configurationService;

        public CategoryDapperQueryService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<List<Category>> GetMainCategories()
        {
            var connectionString = _configurationService.GetSqlServerConnectionString();

            if (string.IsNullOrEmpty(connectionString))
                throw new EmptyConnectionStringException();

            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = "select * FROM CatalogApi_Category where ParentCategoryId is NULL and ProductCounter > 0 and IsVisible = 1";

                connection.Open();

                var result = await connection.QueryAsync<Category>(sqlCommand);

                if (!result.AsList().Any())
                    return new List<Category>();

                return result.ToList();
            }
        }

        public async Task<List<Category>> GetSubCategories(int categoryId)
        {
            var connectionString = _configurationService.GetSqlServerConnectionString();
            var sqlCommand = $"select * FROM CatalogApi_Category where ParentCategoryId = {categoryId} and ProductCounter > 0 and IsVisible = 1";

            return await SqlCallerService.CallDatabase(connectionString, sqlCommand, async connection =>
            {
                var data = await connection.QueryAsync<Category>(sqlCommand);

                return data.ToList();
            });
        }
    }
}
