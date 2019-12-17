using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PromotorApi.Application.SlideShow.Commands.DeleteSlideShow;
using PromotorApi.Mappings;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Tests.Handlers
{
    [TestFixture()]
    public class DeleteSlideShowHandlerTests
    {
        private PromotorContext _context;
        private Serilog.ILogger _logger;

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
        }

        [Test]
        public async Task GdyPoprawnySlideShow_Zwraca_Ok()
        {
            var slideshowList = _context.SlideShows.ToList();
            if (slideshowList.Any())
            {
                _context.SlideShows.RemoveRange(slideshowList);
                _context.SaveChanges();
            }

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));

            var slideshow = _context.SlideShows.FirstOrDefault();
            Assert.That(slideshow, Is.Not.Null);
            Assert.That(slideshow.Id, Is.Not.EqualTo(0));

            var handler = new DeleteSlideShowHandler(_context);

            var command = new DeleteSlideShowCommand();
            command.ScreenId = slideshow.Id;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));
        }

        [Test]
        public async Task GdyNieistniejacySlideShow_Zwraca_Failure()
        {
            var slideshowList = _context.SlideShows.ToList();
            if (slideshowList.Any())
            {
                _context.SlideShows.RemoveRange(slideshowList);
                _context.SaveChanges();
            }

            var handler = new DeleteSlideShowHandler(_context);

            var command = new DeleteSlideShowCommand();
            command.ScreenId = 1000;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));
            _logger.Information(result.Message);
        }

        [Test]
        public async Task GdyUsunietySlideShow_Zwraca_Failure()
        {
            var slideshowList = _context.SlideShows.ToList();
            if (slideshowList.Any())
            {
                _context.SlideShows.RemoveRange(slideshowList);
                _context.SaveChanges();
            }

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Deleted,
                ValidFrom = DateTime.Now
            });
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));

            var slideshow = _context.SlideShows.FirstOrDefault();
            Assert.That(slideshow, Is.Not.Null);
            Assert.That(slideshow.Id, Is.Not.EqualTo(0));

            var handler = new DeleteSlideShowHandler(_context);

            var command = new DeleteSlideShowCommand();
            command.ScreenId = slideshow.Id;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));
            _logger.Information(result.Message);
        }
    }
}
