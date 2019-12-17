using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PromotorApi.Model;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Application.SlideShow.Commands.DeleteSlideShow
{
    public class DeleteSlideShowHandler : IRequestHandler<DeleteSlideShowCommand, MethodResponse>
    {
        private readonly PromotorContext _context;

        public DeleteSlideShowHandler(PromotorContext promotorContext)
        {
            _context = promotorContext;
        }

        public async Task<MethodResponse> Handle(DeleteSlideShowCommand command, CancellationToken cancellationToken)
        {

            var validator = new DeleteSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            if (!result.IsValid)
            {
                return new MethodResponse
                {
                    Status = MethodResponseTypeEnum.Failure,
                    Message = result.Errors.FirstOrDefault()?.ErrorMessage
                };
            }

            var slideShow = await _context.SlideShows.FirstOrDefaultAsync(c => c.Id == command.ScreenId);

            slideShow.LastUpdateDate = DateTime.Now;
            slideShow.Status = (int) SlideShowStatusEnum.Deleted;

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
                    Message = "Wystąpił bład w trakcie usuwania ekranu"
                };
            }

            return new MethodResponse();
        }
    }
}
