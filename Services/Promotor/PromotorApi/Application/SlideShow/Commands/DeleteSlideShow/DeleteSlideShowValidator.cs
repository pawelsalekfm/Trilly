using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PromotorApi.Model;

namespace PromotorApi.Application.SlideShow.Commands.DeleteSlideShow
{
    public class DeleteSlideShowValidator : AbstractValidator<DeleteSlideShowCommand>
    {
        private readonly PromotorContext _context;

        public DeleteSlideShowValidator(PromotorContext context)
        {
            _context = context;

            RuleFor(screen => screen.ScreenId).GreaterThan(0).WithMessage("Proszę podać identyfikator slideshow");
            RuleFor(screen => screen.ScreenId).Must(SlideShowExistsAndReadyToDelete).WithMessage("Wskazany slideshow nie istnieje lub jest już usunięty");
        }

        public bool SlideShowExistsAndReadyToDelete(int slideshowId)
        {
            return _context.SlideShows.Any(c => c.Id == slideshowId && c.Status == (int)SlideShowStatusEnum.Active);
        }
    }
}
