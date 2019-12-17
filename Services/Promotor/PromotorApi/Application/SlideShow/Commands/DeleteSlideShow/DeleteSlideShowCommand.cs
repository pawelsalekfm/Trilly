using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Commands.DeleteSlideShow
{
    public class DeleteSlideShowCommand : IRequest<MethodResponse>
    {
        public int ScreenId { get; set; }
    }
}
