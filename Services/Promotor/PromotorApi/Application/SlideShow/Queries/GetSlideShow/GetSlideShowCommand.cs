using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Trilly.ViewModels.Promotor;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Queries.GetSlideShow
{
    public class GetSlideShowCommand : IRequest<ObjectMethodResponse>
    {
        public int Id { get; set; }
    }
}
