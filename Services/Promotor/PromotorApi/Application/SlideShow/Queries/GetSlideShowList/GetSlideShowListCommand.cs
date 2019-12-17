using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Queries.GetAllSlideShowList
{
    public enum GetSlideShowListCommandMode
    {
        [Description("Wszystkie")]
        All = 1,
        [Description("Aktywne")]
        Active = 2,
        [Description("Usunięte")]
        Deleted = 3,
        [Description("Wygasłe")]
        Expired = 4,
    }

    public class GetSlideShowListCommand : IRequest<ObjectMethodResponse>
    {
        public int Mode { get; set; }
    }
}
