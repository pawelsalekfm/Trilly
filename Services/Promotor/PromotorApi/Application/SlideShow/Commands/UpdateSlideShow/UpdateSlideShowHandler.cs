using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PromotorApi.Application.SlideShow.Commands.DeleteSlideShow;
using PromotorApi.Model;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Commands.UpdateSlideShow
{
    public class UpdateSlideShowHandler : IRequestHandler<UpdateSlideShowCommand, MethodResponse>
    {
        private readonly PromotorContext _context;

        public UpdateSlideShowHandler(PromotorContext promotorContext)
        {
            _context = promotorContext;
        }

        public async Task<MethodResponse> Handle(UpdateSlideShowCommand command, CancellationToken cancellationToken)
        {

            var validator = new UpdateSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            if (!result.IsValid)
            {
                return new MethodResponse
                {
                    Status = MethodResponseTypeEnum.Failure,
                    Message = result.Errors.FirstOrDefault()?.ErrorMessage
                };
            }

            var slideShow = await _context.SlideShows.FirstOrDefaultAsync(c => c.Id == command.Id);

            slideShow.LastUpdateDate = DateTime.Now;
            slideShow.Name = command.Name;
            slideShow.Description = command.Description;
            slideShow.ValidFrom = command.ValidFrom;
            slideShow.ValidTo = command.ValidTo;
            slideShow.LastUpdateDate = DateTime.Now;

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
                    Message = "Wystąpił bład w trakcie aktualizacji ekranu"
                };
            }

            return new MethodResponse();
        }
    }
}
