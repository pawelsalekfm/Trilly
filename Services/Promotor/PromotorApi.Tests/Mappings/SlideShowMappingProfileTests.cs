using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromotorApi.Application.SlideShow.Commands.CreateSlideShow;
using PromotorApi.Mappings;
using Serilog;

namespace PromotorApi.Tests.Mappings
{
    [TestFixture()]
    public class SlideShowMappingProfileTests
    {
        private Serilog.ILogger _logger;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _logger = _logger = new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .CreateLogger();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<SlideShowMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void DodawanieSlideShow_GdyPoprawny_Ok()
        {
            var command = new CreateSlideShowCommand();
            command.Name = $"Nazwa {Guid.NewGuid().ToString()}";
            command.ValidFrom = DateTime.Now;
            command.Description = $"Testowy opis {Guid.NewGuid().ToString()}";

            var slideshow = _mapper.Map<Model.SlideShow>(command);

            Assert.That(slideshow.Name, Is.EqualTo(command.Name));
            Assert.That(slideshow.Description, Is.EqualTo(command.Description));
            Assert.That(slideshow.ValidFrom, Is.EqualTo(command.ValidFrom));

            _logger.Information(slideshow.Name);
            _logger.Information(slideshow.Description);
            _logger.Information(slideshow.ValidFrom.ToString());
        }
    }
}
