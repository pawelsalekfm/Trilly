using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PromotorApi.Model;
using Trilly.ViewModels.Promotor;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Queries.GetSlideShow
{
    public class GetSlideShowValidator : AbstractValidator<GetSlideShowCommand>
    {
        private readonly PromotorContext _context;

        public GetSlideShowValidator(PromotorContext context)
        {
            _context = context;

            RuleFor(screen => screen.Id).GreaterThan(0).WithMessage("Proszę podać identyfikator slideshow");
            RuleFor(screen => screen.Id).Must(SlideShowExistsAndReadyToGet).WithMessage("Wskazany slideshow nie istnieje");
        }

        public bool SlideShowExistsAndReadyToGet(int slideshowId)
        {
            return _context.SlideShows.Any(c => c.Id == slideshowId && c.Status == (int)SlideShowStatusEnum.Active);
        }
    }
}
