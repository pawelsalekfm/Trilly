using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatalogApi.Models;
using CatalogApi.Services;
using MediatR;
using Trilly.Tools.Exceptions;

namespace CatalogApi.Application.Category.GetMainCategories
{
    public class GetMainCategoriesHandler : IRequestHandler<GetMainCategoriesCommand, GetMainCategoriesDTO>
    {
        private readonly ICategoryDapperQueryService _categoryDapperQueryService;

        public GetMainCategoriesHandler(ICategoryDapperQueryService categoryDapperQueryService)
        {
            _categoryDapperQueryService = categoryDapperQueryService;
        }

        public async Task<GetMainCategoriesDTO> Handle(GetMainCategoriesCommand command, CancellationToken cancellationToken)
        {
            var response = new GetMainCategoriesDTO();

            try
            {
                response.Categories = await _categoryDapperQueryService.GetMainCategories();
            }
            catch (EmptyConnectionStringException emptyConnectionStringException)
            {
                Console.WriteLine(emptyConnectionStringException);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return response;
        }
    }

    public class GetMainCategoriesDTO
    {
        public List<Models.Category> Categories { get; set; }

        public GetMainCategoriesDTO()
        {
            Categories = new List<Models.Category>();
        }
    }
}
