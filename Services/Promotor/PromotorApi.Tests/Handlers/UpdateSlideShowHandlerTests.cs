using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PromotorApi.Application.SlideShow.Commands.DeleteSlideShow;
using PromotorApi.Application.SlideShow.Commands.UpdateSlideShow;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Tests.Handlers
{
    [TestFixture()]
    public class UpdateSlideShowHandlerTests
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

            var handler = new UpdateSlideShowHandler(_context);

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = DateTime.Now.AddDays(7);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));

            var updatedSlideShow = _context.SlideShows.FirstOrDefault(c => c.Id == command.Id);

            Assert.That(updatedSlideShow.Name, Is.EqualTo(command.Name));
            Assert.That(updatedSlideShow.Description, Is.EqualTo(command.Description));
            Assert.That(updatedSlideShow.ValidFrom, Is.EqualTo(command.ValidFrom));
            Assert.That(updatedSlideShow.ValidTo, Is.EqualTo(command.ValidTo));
        }

        [Test]
        public async Task GdyPoprawnySlideShowBezValidTo_Zwraca_Ok()
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
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(7)
            });
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));

            var slideshow = _context.SlideShows.FirstOrDefault();
            Assert.That(slideshow, Is.Not.Null);
            Assert.That(slideshow.Id, Is.Not.EqualTo(0));

            var handler = new UpdateSlideShowHandler(_context);

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = null;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));

            var updatedSlideShow = _context.SlideShows.FirstOrDefault(c => c.Id == command.Id);

            Assert.That(updatedSlideShow.Name, Is.EqualTo(command.Name));
            Assert.That(updatedSlideShow.Description, Is.EqualTo(command.Description));
            Assert.That(updatedSlideShow.ValidFrom, Is.EqualTo(command.ValidFrom));
            Assert.That(updatedSlideShow.ValidTo, Is.EqualTo(command.ValidTo));
        }

        [Test]
        public async Task GdyNiepoprawnySlideShowBezId_Zwraca_Failure()
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
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(7)
            });
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));

            var slideshow = _context.SlideShows.FirstOrDefault();
            Assert.That(slideshow, Is.Not.Null);
            Assert.That(slideshow.Id, Is.Not.EqualTo(0));

            var handler = new UpdateSlideShowHandler(_context);

            var command = new UpdateSlideShowCommand();
            //command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = null;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));

            _logger.Information(result.Message);
        }

        [Test]
        public async Task GdyNiepoprawnySlideShowBezNazwy_Zwraca_Failure()
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
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(7)
            });
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));

            var slideshow = _context.SlideShows.FirstOrDefault();
            Assert.That(slideshow, Is.Not.Null);
            Assert.That(slideshow.Id, Is.Not.EqualTo(0));

            var handler = new UpdateSlideShowHandler(_context);

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            //command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = null;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));

            _logger.Information(result.Message);
        }

        [Test]
        public async Task GdyNiepoprawnieZdefiniowanaDataWaznosciSlideShow_Failure()
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
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(7)
            });
            _context.SaveChanges();

            Assert.That(_context.SlideShows.Count(), Is.EqualTo(1));

            var slideshow = _context.SlideShows.FirstOrDefault();
            Assert.That(slideshow, Is.Not.Null);
            Assert.That(slideshow.Id, Is.Not.EqualTo(0));

            var handler = new UpdateSlideShowHandler(_context);

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = DateTime.Now.AddDays(-7);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));

            _logger.Information(result.Message);
        }
    }
}
