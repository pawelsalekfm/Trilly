using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PromotorApi.Application.SlideShow.Queries.GetSlideShow;
using PromotorApi.Application.SlideShow.Queries.GetSlideShowList;
using PromotorApi.Model;
using Trilly.ViewModels.Promotor;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Queries.GetAllSlideShowList
{
    public class GetSlideShowListHandler : IRequestHandler<GetSlideShowListCommand, ObjectMethodResponse>
    {
        private readonly PromotorContext _context;
        private readonly IMapper _mapper;

        public GetSlideShowListHandler(PromotorContext promotorContext, IMapper mapper)
        {
            _context = promotorContext;
            _mapper = mapper;
        }

        public async Task<ObjectMethodResponse> Handle(GetSlideShowListCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetSlideShowListValidator();
            ValidationResult result = validator.Validate(command);

            if (!result.IsValid)
            {
                return new ObjectMethodResponse
                {
                    Status = MethodResponseTypeEnum.Failure,
                    Message = result.Errors.FirstOrDefault()?.ErrorMessage
                };
            }

            var slideShowList =  _context.SlideShows.AsNoTracking();
            var selectedSlidesList = new List<Model.SlideShow>();

            var mode = Enum.Parse<GetSlideShowListCommandMode>(Convert.ToString(command.Mode));

            switch (mode)
            {
                case GetSlideShowListCommandMode.All:
                    selectedSlidesList = await slideShowList.ToListAsync(cancellationToken: cancellationToken);
                    break;

                case GetSlideShowListCommandMode.Active:
                    selectedSlidesList = await slideShowList.Where(c => c.Status == (int) SlideShowStatusEnum.Active && c.ValidTo.Value.Date > DateTime.Now).ToListAsync(cancellationToken: cancellationToken);
                    break;

                case GetSlideShowListCommandMode.Deleted:
                    selectedSlidesList = await slideShowList.Where(c => c.Status == (int)SlideShowStatusEnum.Deleted).ToListAsync(cancellationToken: cancellationToken);
                    break;

                case GetSlideShowListCommandMode.Expired:
                    selectedSlidesList = await slideShowList.Where(c => c.Status == (int)SlideShowStatusEnum.Active && c.ValidTo.Value.Date < DateTime.Now.Date).ToListAsync(cancellationToken: cancellationToken);
                    break;
            }

            var slideshowVm = _mapper.Map<List<Model.SlideShow>, List<SlideShowListItemVm>>(selectedSlidesList);

            return new ObjectMethodResponse(slideshowVm);
        }
    }
}
