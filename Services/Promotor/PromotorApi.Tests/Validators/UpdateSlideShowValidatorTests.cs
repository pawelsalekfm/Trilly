using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PromotorApi.Application.SlideShow.Commands.UpdateSlideShow;
using PromotorApi.Model;
using Serilog;

namespace PromotorApi.Tests.Validators
{
    [TestFixture]
    public class UpdateSlideShowValidatorTests
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
        public void GdyPoprawnyCommand_walidacjazwraca_Ok()
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

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = DateTime.Now.AddDays(7);

            var validator = new UpdateSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void GdyBrakIdSlideshowCommand_walidacjazwraca_Failure()
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

            var command = new UpdateSlideShowCommand();
            //command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = DateTime.Now.AddDays(7);

            var validator = new UpdateSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.False);

            foreach (var validationFailure in result.Errors)
            {
               _logger.Information(validationFailure.ErrorMessage); 
            }
        }

        [Test]
        public void GdyPoprawnyBezValidToSlideshowCommand_walidacjazwraca_Ok()
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

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = null;

            var validator = new UpdateSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.False);

            foreach (var validationFailure in result.Errors)
            {
                _logger.Information(validationFailure.ErrorMessage);
            }
        }

        [Test]
        public void GdyBrakNazwySlideshowCommand_walidacjazwraca_Failure()
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

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            //command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = DateTime.Now.AddDays(7);

            var validator = new UpdateSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.False);

            foreach (var validationFailure in result.Errors)
            {
                _logger.Information(validationFailure.ErrorMessage);
            }
        }

        [Test]
        public void GdyBrakValidToJestWczesniejNizValidFromSlideshowCommand_walidacjazwraca_Failure()
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

            var command = new UpdateSlideShowCommand();
            command.Id = slideshow.Id;
            command.Name = $"Opis {Guid.NewGuid().ToString()}";
            command.Description = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.ValidTo = DateTime.Now.AddDays(-7);

            var validator = new UpdateSlideShowValidator(_context);
            ValidationResult result = validator.Validate(command);

            Assert.That(result.IsValid, Is.False);

            foreach (var validationFailure in result.Errors)
            {
                _logger.Information(validationFailure.ErrorMessage);
            }
        }
    }
}
