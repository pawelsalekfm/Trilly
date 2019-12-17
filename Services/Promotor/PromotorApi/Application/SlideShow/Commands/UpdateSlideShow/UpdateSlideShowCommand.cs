using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Commands.UpdateSlideShow
{
    public class UpdateSlideShowCommand : IRequest<MethodResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
