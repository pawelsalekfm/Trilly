using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CatalogApi;
using Grpc.Core;
using Grpc.Net.Client;
using Web.GatewayApi.ApiModels.Category;
using GetMainCategoriesResponse = Web.GatewayApi.ApiModels.Category.GetMainCategoriesResponse;
using GetSubCategoriesResponse = Web.GatewayApi.ApiModels.Category.GetSubCategoriesResponse;

namespace Web.GatewayApi.Services
{
    public interface ICatalogService
    {
        Task<GetMainCategoriesResponse> GetMainCategories();
        Task<GetSubCategoriesResponse> GetSubCategories(int id);
    }

    public class CatalogService : ICatalogService
    {
        public readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region GetMainCategories
        public async Task<GetMainCategoriesResponse> GetMainCategories()
        {
            return await GrpcCallerService.CallService("http://trilly.catalogapi:55001", async channel =>
            {
                var client = new CategoryGrpc.CategoryGrpcClient(channel);

                var reply = await client.GetMainCategoriesAsync(new GetMainCategoriesRequest());

                return MapMainCategoriesToGetMainCategoriesResponse(reply);
            });
        }

        private GetMainCategoriesResponse MapMainCategoriesToGetMainCategoriesResponse(CatalogApi.GetMainCategoriesResponse categories)
        {
            var response = new GetMainCategoriesResponse();

            if (categories == null)
                return response;

            if (categories.Count == 0)
                return response;

            foreach (var categoryShortInfoModel in categories.Categories)
            {
                response.Categories.Add(new CategoryShortInfoModel
                {
                    Id = categoryShortInfoModel.Id,
                    Name = categoryShortInfoModel.Name,
                    Slug = categoryShortInfoModel.Slug
                });
            }

            response.Count = response.Categories.Count;

            return response;
        }
        #endregion

        #region GetSubCategories
        public async Task<ApiModels.Category.GetSubCategoriesResponse> GetSubCategories(int categoryId)
        {
            return await GrpcCallerService.CallService("http://trilly.catalogapi:55001", async channel =>
            {
                CategoryGrpc.CategoryGrpcClient client = new CategoryGrpc.CategoryGrpcClient(channel);

                CatalogApi.GetSubCategoriesResponse reply = await client.GetSubCategoriesAsync(new CatalogApi.GetSubCategoriesRequest
                {
                    CategoryId = categoryId
                });

                return MapSubCategoriesToGetSubCategoriesResponse(reply);
            });
        }

        private ApiModels.Category.GetSubCategoriesResponse MapSubCategoriesToGetSubCategoriesResponse(CatalogApi.GetSubCategoriesResponse categories)
        {
            var response = new GetSubCategoriesResponse();

            if (categories == null)
                return response;

            if (categories.Count == 0)
                return response;

            foreach (var categoryShortInfoModel in categories.Categories)
            {
                response.Categories.Add(new CategoryShortInfoModel
                {
                    Id = categoryShortInfoModel.Id,
                    Name = categoryShortInfoModel.Name,
                    Slug = categoryShortInfoModel.Slug
                });
            }

            response.Count = response.Categories.Count;

            return response;
        }
        
        #endregion
    }
}
