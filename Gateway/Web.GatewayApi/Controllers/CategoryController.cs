using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.GatewayApi.ApiModels.Category;
using Web.GatewayApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CategoryController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        /// <summary>
        /// Zwraca listę kategorii głównych
        /// </summary>
        /// <returns></returns>
        [HttpGet("main")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMainCategoriesResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMainCategoriesResponse>> GetMainCategories()
        {
            try
            {
                return await _catalogService.GetMainCategories();
            }
            catch(Exception ex)
            {
                return BadRequest("Error in api");
            }
        }

        /// <summary>
        /// Pobiera listę kategorii dla wskazanej kategorii
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("subcategories/{categoryId:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSubCategoriesResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSubCategoriesResponse>> GetMainCategories(int categoryId)
        {
            try
            {
                return await _catalogService.GetSubCategories(categoryId);
            }
            catch (Exception ex)
            {
                return BadRequest("Error in api");
            }
        }
    }
}
