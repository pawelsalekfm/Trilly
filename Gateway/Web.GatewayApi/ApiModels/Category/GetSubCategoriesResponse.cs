using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GatewayApi.ApiModels.Category
{
    public class GetSubCategoriesResponse
    {
        public int Count { get; set; }
        public List<CategoryShortInfoModel> Categories { get; set; }

        public GetSubCategoriesResponse()
        {
            Categories = new List<CategoryShortInfoModel>();
        }
    }
}
