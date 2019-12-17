using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using PromotorApi.Model;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Commands.CreateSlideShow
{
    public class CreateSlideShowHandler : IRequestHandler<CreateSlideShowCommand, MethodResponse>
    {
        private readonly PromotorContext _context;
        private readonly IMapper _mapper;

        public CreateSlideShowHandler(PromotorContext promotorContext, IMapper mapper)
        {
            _context = promotorContext;
            _mapper = mapper;
        }

        public async Task<MethodResponse> Handle(CreateSlideShowCommand command, CancellationToken cancellationToken)
        {
            var slideshow = _mapper.Map<Model.SlideShow>(command);

            var validator = new CreateSlideShowValidator();
            ValidationResult result = validator.Validate(slideshow);

            if (!result.IsValid)
            {
                return new MethodResponse
                {
                   Status = MethodResponseTypeEnum.Failure,
                   Message = result.Errors.FirstOrDefault()?.ErrorMessage
                };
            }

            slideshow.CreationDate = DateTime.Now;
            slideshow.Status = (int) SlideShowStatusEnum.Active;

            await _context.AddAsync(slideshow);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new MethodResponse
                {
                    Status = MethodResponseTypeEnum.Exception,
                    Exception = e,
                    Message = "Wystąpił bład w trakcie dodawania ekranu"
                };
            }

            return new MethodResponse();
        }
    }
}
