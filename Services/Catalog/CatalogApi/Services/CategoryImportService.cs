using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using Trilly.ViewModels.Category;

namespace CatalogApi.Services
{
    public interface ICategoryImportService
    {
        Task AddCategoryList(List<JsonCategoryImportVM> categories);
    }

    public class CategoryImportService : ICategoryImportService
    {
        private readonly CatalogContext _context;
        private readonly IMapper _mapper;

        public CategoryImportService(CatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCategoryList(List<JsonCategoryImportVM> categories)
        {
            if (!categories.Any())
                return;

            try
            {
                foreach (var jsonCategoryImportVm in categories)
                {
                    Console.WriteLine($"Import kategorii: {jsonCategoryImportVm.XlId}:{jsonCategoryImportVm.Name}");
                    var category = _mapper.Map<Category>(jsonCategoryImportVm);
                    category.IsVisible = true;

                    if (_context.Categories.Any(c => c.ExternalId == category.ExternalId))
                        continue;

                    _context.Categories.Add(category);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                var categoriesWithParent = categories.Where(c => c.ParentId != null).ToList();

                if (!categoriesWithParent.Any())
                    return;

                foreach (var jsonCategoryImportVm in categoriesWithParent)
                {
                    Console.WriteLine($"Wyszukanie rodzica: {jsonCategoryImportVm.XlId}:{jsonCategoryImportVm.Name}");
                    var category = _context.Categories.FirstOrDefault(c => c.ExternalId == jsonCategoryImportVm.XlId);

                    var parentJson = categories.FirstOrDefault(c => c.Id == jsonCategoryImportVm.ParentId);

                    if (category == null)
                        continue;

                    if (parentJson == null)
                        continue;

                    var parent = _context.Categories.FirstOrDefault(c => c.ExternalId == parentJson.XlId);

                    if (parent != null)
                        category.ParentCategoryId = parent?.Id;
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
