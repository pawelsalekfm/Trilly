using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Application.Category.GetMainCategories;
using CatalogApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ICategoryDapperQueryService _categoryDapperQueryService;

        public HomeController(ICategoryDapperQueryService categoryDapperQueryService)
        {
            _categoryDapperQueryService = categoryDapperQueryService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            //_logger.Information("{MessageType} - {Body}", "HomeController", "Hello from CategoryApi!");
            return Content($"Hello from CatalogApi: {DateTime.Now}");
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = new GetMainCategoriesDTO();

            response.Categories = await _categoryDapperQueryService.GetMainCategories();

            return new JsonResult(response);
            //_logger.Information("{MessageType} - {Body}", "HomeController", "Hello from CategoryApi!");
            //return Content($"Hello from CatalogApi: {DateTime.Now}");
        }
    }
}
