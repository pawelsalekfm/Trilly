using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi;
using CatalogApi.Application.Category.GetMainCategories;
using CatalogApi.Application.Category.GetSubcategory;
using Grpc.Core;
using MediatR;

namespace CatalogApi.Services.Grpc
{
    public class CategoryGrpcService : CategoryGrpc.CategoryGrpcBase
    {
        private readonly IMediator _mediator;

        public CategoryGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetMainCategoriesResponse> GetMainCategories(GetMainCategoriesRequest request, ServerCallContext context)
        {
            var categoriesDTO = await _mediator.Send(new GetMainCategoriesCommand());

            var response = new GetMainCategoriesResponse();

            if (categoriesDTO.Categories.Any())
            {
                foreach (var categoriesDtoCategory in categoriesDTO.Categories)
                {
                    response.Categories.Add(new CategoryObject
                    {
                        Id = categoriesDtoCategory.Id,
                        Name = categoriesDtoCategory.Name,
                        Slug = categoriesDtoCategory.Slug
                    });
                }

                response.Count = response.Categories.Count;
            }

            return response;
        }

        public override async Task<GetSubCategoriesResponse> GetSubCategories(GetSubCategoriesRequest request,
            ServerCallContext context)
        {
            var subCategoriesDto = await _mediator.Send(new GetSubcategoryCommand(request.CategoryId));
            var response = new GetSubCategoriesResponse();

            if (subCategoriesDto.Categories.Any())
            {
                foreach (var category in subCategoriesDto.Categories)
                {
                    response.Categories.Add(new CategoryObject
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Slug = category.Slug
                    });
                }

                response.Count = response.Categories.Count;
            }

            return response;
        }
    }
}
