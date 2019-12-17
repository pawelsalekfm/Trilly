using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromotorApi.Application.SlideShow.Commands.CreateSlideShow;
using PromotorApi.Mappings;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Tests
{
    [TestFixture()]
    public class CreateSlideShowHandlerTests
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
                opts.AddProfile<SlideShowMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task GdyDodajePoprawnySlideShow_Zwraca_Ok()
        {
            var command = new CreateSlideShowCommand();
            command.Name = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.Description = $"Testowy opis {Guid.NewGuid().ToString()}";

            var handler = new CreateSlideShowHandler(_context, _mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));

            var slideshow = await _context.SlideShows.FirstOrDefaultAsync(c => c.Name.Equals(command.Name));

            Assert.That(slideshow, Is.Not.Null);

            _logger.Information(slideshow.Name);
            _logger.Information(slideshow.Description);
            _logger.Information(slideshow.CreationDate.ToString());
        }

        [Test]
        public async Task GdySlideShowBezNazwy_Zwraca_Failure()
        {
            var command = new CreateSlideShowCommand();
            command.ValidFrom = DateTime.Now;
            command.Description = $"Testowy opis {Guid.NewGuid().ToString()}";

            var handler = new CreateSlideShowHandler(_context, _mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));

            var slideshow = await _context.SlideShows.FirstOrDefaultAsync(c => c.Name.Equals(command.Name));

            Assert.That(slideshow, Is.Null);

            Assert.That(result.Message, Is.Not.Empty);

            _logger.Error($"MessageResponse: {result.Message}");
        }

        [Test]
        public async Task GdySlideShowBezDatyWaznosci_Zwraca_Failure()
        {
            var command = new CreateSlideShowCommand();
            command.Name = $"Nazwa {Guid.NewGuid().ToString()}";
            command.Description = $"Testowy opis {Guid.NewGuid().ToString()}";

            var handler = new CreateSlideShowHandler(_context, _mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));

            var slideshow = await _context.SlideShows.FirstOrDefaultAsync(c => c.Name.Equals(command.Name));

            Assert.That(slideshow, Is.Null);

            Assert.That(result.Message, Is.Not.Empty);

            _logger.Error($"MessageResponse: {result.Message}");
        }

        [Test]
        public async Task GdySlideShowBezDatyWaznosciPrzedwczoraj_Zwraca_Failure()
        {
            var command = new CreateSlideShowCommand();
            command.Name = $"Nazwa {Guid.NewGuid().ToString()}";
            command.Description = $"Testowy opis {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now.AddDays(-2);

            var handler = new CreateSlideShowHandler(_context, _mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.That(result.Status, Is.EqualTo(MethodResponseTypeEnum.Failure));

            var slideshow = await _context.SlideShows.FirstOrDefaultAsync(c => c.Name.Equals(command.Name));

            Assert.That(slideshow, Is.Null);

            Assert.That(result.Message, Is.Not.Empty);

            _logger.Error($"MessageResponse: {result.Message}");
        }
    }
}
