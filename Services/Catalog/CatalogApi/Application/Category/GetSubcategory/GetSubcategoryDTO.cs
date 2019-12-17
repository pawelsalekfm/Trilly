using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Application.Category.GetSubcategory
{
    public class GetSubcategoryDTO
    {
        public int CategoryId { get; set; }
        public List<Models.Category> Categories { get; set; }

        public GetSubcategoryDTO()
        {
            Categories = new List<Models.Category>();
        }
    }
}
