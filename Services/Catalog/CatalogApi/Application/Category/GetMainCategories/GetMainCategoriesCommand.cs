using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CatalogApi.Application.Category.GetMainCategories
{
    public class GetMainCategoriesCommand : IRequest<GetMainCategoriesDTO>
    {
        public GetMainCategoriesCommand()
        {
        }
    }
}
