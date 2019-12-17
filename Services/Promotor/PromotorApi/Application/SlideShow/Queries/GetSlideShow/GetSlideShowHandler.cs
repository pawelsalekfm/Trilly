using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PromotorApi.Application.SlideShow.Commands.DeleteSlideShow;
using PromotorApi.Model;
using Trilly.ViewModels.Promotor;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Queries.GetSlideShow
{
    public class GetSlideShowHandler : IRequestHandler<GetSlideShowCommand, ObjectMethodResponse>
    {
        private readonly PromotorContext _context;
        private readonly IMapper _mapper;

        public GetSlideShowHandler(PromotorContext promotorContext, IMapper mapper)
        {
            _context = promotorContext;
            _mapper = mapper;
        }

        public async Task<ObjectMethodResponse> Handle(GetSlideShowCommand command, CancellationToken cancellationToken)
        {

            var validator = new GetSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            if (!result.IsValid)
            {
                return new ObjectMethodResponse
                {
                    Status = MethodResponseTypeEnum.Failure,
                    Message = result.Errors.FirstOrDefault()?.ErrorMessage
                };
            }

            var slideShow = await _context.SlideShows.AsNoTracking()
                .Include(c=>c.Slides)
                .FirstOrDefaultAsync(c => c.Id == command.Id);

            slideShow.Slides = slideShow.Slides.OrderBy(c => c.Order).ToList();

            var slideshowVm = _mapper.Map<Model.SlideShow, SlideShowVm>(slideShow);

            return new ObjectMethodResponse(slideshowVm);
        }
    }
}
