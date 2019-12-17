using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromotorApi.Application.SlideShow.Commands.DeleteSlideShow;
using PromotorApi.Model;
using Serilog;

namespace PromotorApi.Tests.Validators
{
    [TestFixture()]
    public class DeleteSlideShowValidatorTests
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
        public void GdyWalidujemyPoprawnySlideShow_Zwraca_Ok()
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

            var command = new DeleteSlideShowCommand();
            command.ScreenId = slideshow.Id;

            var validator = new DeleteSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void GdyWalidujemyNieistniejacySlideShow_Zwraca_Failure()
        {
            var slideshowList = _context.SlideShows.ToList();
            if (slideshowList.Any())
            {
                _context.SlideShows.RemoveRange(slideshowList);
                _context.SaveChanges();
            }

            var command = new DeleteSlideShowCommand();
            command.ScreenId = 100;

            var validator = new DeleteSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.False);

            foreach (var validationFailure in result.Errors)
            {
                _logger.Information(validationFailure.ErrorMessage);
            }
        }

        [Test]
        public void GdyWalidujemyUsunietySlideShow_Zwraca_Failure()
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

            var command = new DeleteSlideShowCommand();
            command.ScreenId = slideshow.Id;

            var validator = new DeleteSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.False);

            foreach (var validationFailure in result.Errors)
            {
                _logger.Information(validationFailure.ErrorMessage);
            }
        }
    }
}
