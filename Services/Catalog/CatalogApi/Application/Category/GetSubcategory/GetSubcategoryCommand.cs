using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Conventions;
using MediatR;

namespace CatalogApi.Application.Category.GetSubcategory
{
    public class GetSubcategoryCommand : IRequest<GetSubcategoryDTO>
    {
        public int CategoryId { get; set; }

        public GetSubcategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
