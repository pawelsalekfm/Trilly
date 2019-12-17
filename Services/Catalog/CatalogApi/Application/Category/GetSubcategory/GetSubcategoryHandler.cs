using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatalogApi.Application.Category.GetMainCategories;
using CatalogApi.Services;
using MediatR;
using Trilly.Tools.Exceptions;

namespace CatalogApi.Application.Category.GetSubcategory
{
    public class GetSubcategoryHandler : IRequestHandler<GetSubcategoryCommand, GetSubcategoryDTO>
    {
        private readonly ICategoryDapperQueryService _categoryDapperQueryService;

        public GetSubcategoryHandler(ICategoryDapperQueryService categoryDapperQueryService)
        {
            _categoryDapperQueryService = categoryDapperQueryService;
        }

        public async Task<GetSubcategoryDTO> Handle(GetSubcategoryCommand command, CancellationToken cancellationToken)
        {
            var response = new GetSubcategoryDTO();
            response.CategoryId = command.CategoryId;

            try
            {
                response.Categories = await _categoryDapperQueryService.GetSubCategories(command.CategoryId);
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
}
