using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromotorApi.Application.SlideShow.Queries.GetSlideShow;
using PromotorApi.Mappings;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Promotor;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Tests.Handlers
{
    [TestFixture()]
    public class GetSlideShowHandlerTests
    {
        private PromotorContext _context;
        private Serilog.ILogger _logger;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _logger = _logger = new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .CreateLogger();

            var options = new DbContextOptionsBuilder<PromotorContext>()
                .UseInMemoryDatabase(databaseName: "TrillyTestDb")
                .Options;

            _context = new PromotorContext(options);

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<SlideShowToVmMappingProfile>();
                opts.AddProfile<SlideShowItemToVmMappingProfile>();
                opts.AddProfile<SlideShowMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task GdyNiepoprawnyId_zwraca_Failure()
        {
            var slideshowList = _context.SlideShows.ToList();
            if (slideshowList.Any())
            {
                _context.SlideShows.RemoveRange(slideshowList);
                _context.SaveChanges();
            }

            var handler = new GetSlideShowHandler(_context, _mapper);
            var command = new GetSlideShowCommand();
            command.Id = 1000;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));
            Assert.That(result.ObjectResponse, Is.Null);

            _logger.Information(result.Message);
        }

        [Test]
        public async Task GdyPoprawnyId_Zwraca_Ok()
        {
            var slideshowList = _context.SlideShows.ToList();
            if (slideshowList.Any())
            {
                _context.SlideShows.RemoveRange(slideshowList);
                _context.SaveChanges();
            }

            var slide = new Model.SlideShow();
            slide.CreationDate = DateTime.Now;
            slide.Description = $"Opis: {Guid.NewGuid().ToString()}";
            slide.Name = $"Nazwa: {Guid.NewGuid().ToString()}";
            slide.ValidFrom = DateTime.Now;
            slide.Status = (int) SlideShowStatusEnum.Active;

            slide.Slides.Add(new SlideShowItem
            {
                Content = $"Treść {Guid.NewGuid().ToString()}",
                Description = $"Opis widoku: {Guid.NewGuid().ToString()}",
                Id = 1,
                Name = $"Nazwa widoku: {Guid.NewGuid().ToString()}",
                Order = 1,
            });

            slide.Slides.Add(new SlideShowItem
            {
                Content = $"Treść {Guid.NewGuid().ToString()}",
                Description = $"Opis widoku: {Guid.NewGuid().ToString()}",
                Id = 2,
                Name = $"Nazwa widoku: {Guid.NewGuid().ToString()}",
                Order = 2,
            });

            slide.Slides.Add(new SlideShowItem
            {
                Content = $"Treść {Guid.NewGuid().ToString()}",
                Description = $"Opis widoku: {Guid.NewGuid().ToString()}",
                Id = 3,
                Name = $"Nazwa widoku: {Guid.NewGuid().ToString()}",
                Order = 3,
            });

            _context.SlideShows.Add(slide);
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));
            Assert.That(_context.SlideShowItems.Count(), Is.EqualTo(3));

            var slideFromDb = _context.SlideShows.FirstOrDefault();

            var handler = new GetSlideShowHandler(_context, _mapper);
            var command = new GetSlideShowCommand();
            command.Id = slideFromDb.Id;

            var result = await handler.Handle(command, CancellationToken.None);
            _logger.Information(result.Message);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));
            Assert.That(result.ObjectResponse, Is.Not.Null);

            var slideVm = result.ObjectResponse as SlideShowVm;

            Assert.That(slideVm.Slides.Count(), Is.EqualTo(slide.Slides.Count));
            Assert.That(slideVm.Id, Is.EqualTo(slide.Id));
        }
    }
}
