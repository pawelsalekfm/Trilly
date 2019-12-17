using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Models;

namespace CatalogApi.Services
{
    public interface ICategoryService
    {
        Task<Category> GetMainCategories();
    }

    public class CategoryService : ICategoryService
    {
        
        public CategoryService()
        {
            
        }

        public async Task<Category> GetMainCategories()
        {
            throw new NotImplementedException();
        }
    }
}
