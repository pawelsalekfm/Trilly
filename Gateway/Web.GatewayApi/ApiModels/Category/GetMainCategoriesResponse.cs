using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GatewayApi.ApiModels.Category
{
    public class GetMainCategoriesResponse
    {
        public int Count { get; set; }
        public List<CategoryShortInfoModel> Categories { get; set; }

        public GetMainCategoriesResponse()
        {
            Categories = new List<CategoryShortInfoModel>();
        }
    }
}
