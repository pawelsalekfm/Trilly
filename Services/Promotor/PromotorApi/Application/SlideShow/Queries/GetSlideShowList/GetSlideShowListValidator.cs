using System;
using EnumsNET;
using FluentValidation;
using PromotorApi.Application.SlideShow.Queries.GetAllSlideShowList;

namespace PromotorApi.Application.SlideShow.Queries.GetSlideShowList
{
    public class GetSlideShowListValidator : AbstractValidator<GetSlideShowListCommand>
    {
        public GetSlideShowListValidator()
        {
            RuleFor(screen => screen.Mode).Must(IsValidEnumMember).WithMessage("Niepoprawny parametr generowania listy slajdów");
        }

        public bool IsValidEnumMember(int mode)
        {
            return Enum.Parse<GetSlideShowListCommandMode>(Convert.ToString(mode)).IsValid();
        }
    }
}
