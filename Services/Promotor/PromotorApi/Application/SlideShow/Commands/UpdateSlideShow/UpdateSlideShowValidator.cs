using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PromotorApi.Model;

namespace PromotorApi.Application.SlideShow.Commands.UpdateSlideShow
{
    public class UpdateSlideShowValidator : AbstractValidator<UpdateSlideShowCommand>
    {
        private readonly PromotorContext _context;
        public UpdateSlideShowValidator(PromotorContext context)
        {
            _context = context;

            RuleFor(screen => screen.Id).GreaterThan(0).WithMessage("Proszę podać identyfikator slideshow");
            RuleFor(screen => screen.Id).Must(SlideShowExistsAndReadyToUpdate).WithMessage("Wskazany slideshow nie istnieje lub został usunięty");
            RuleFor(screen => screen.Name).NotEmpty().WithMessage("Proszę podać nazwę slideshow");
            RuleFor(screen => screen).Must(SlideShowDatesCondition).WithMessage(
                "Data zakończenia prezentacji slideshow jest wcześniejsza niż data rozpoczęcia prezentacji");
        }

        public bool SlideShowExistsAndReadyToUpdate(int slideshowId)
        {
            return _context.SlideShows.Any(c => c.Id == slideshowId && c.Status == (int)SlideShowStatusEnum.Active);
        }

        public bool SlideShowDatesCondition(UpdateSlideShowCommand command)
        {
            if (command.ValidTo == null)
                return true;

            //ValidTo is before ValidFrom
            if (DateTime.Compare(command.ValidFrom.Date, (DateTime) command.ValidTo.Value.Date) > 0)
                return false;

            return true;
        }
    }
}
