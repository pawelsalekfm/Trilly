using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace PromotorApi.Application.SlideShow.Commands.CreateSlideShow
{
    public class CreateSlideShowValidator : AbstractValidator<Model.SlideShow>
    {
        public CreateSlideShowValidator()
        {
            RuleFor(screen => screen.Name).NotEmpty().WithMessage("Proszę podać nazwę slideshow");
            RuleFor(screen => screen.ValidFrom).NotNull().WithMessage("Proszę określić datę od kiedy slideshow ma być widoczny");
            RuleFor(screen => screen.ValidFrom).NotNull().GreaterThan(DateTime.Now.AddDays(-1)).WithMessage("Proszę określić późniejszą datę ważności slideshow");
        }
    }
}
