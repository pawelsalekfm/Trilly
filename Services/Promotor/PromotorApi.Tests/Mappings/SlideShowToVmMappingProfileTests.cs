using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromotorApi.Mappings;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Promotor;

namespace PromotorApi.Tests.Mappings
{
    [TestFixture()]
    public class SlideShowToVmMappingProfileTests
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
                opts.AddProfile<SlideShowItemToVmMappingProfile>();
                opts.AddProfile<SlideShowToVmMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void DodawanieSlideShow_GdyPoprawny_Ok()
        {
            var slide = new Model.SlideShow();
            slide.Id = 1;
            slide.CreationDate = DateTime.Now;
            slide.Description = $"Opis: {Guid.NewGuid().ToString()}";
            slide.Name = $"Nazwa: {Guid.NewGuid().ToString()}";
            slide.ValidFrom = DateTime.Now;

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


            var slidevm = _mapper.Map<Model.SlideShow, SlideShowVm>(slide);

            Assert.That(slidevm.Name, Is.EqualTo(slide.Name));
            Assert.That(slidevm.Description, Is.EqualTo(slide.Description));
            Assert.That(slidevm.ValidFrom, Is.EqualTo(slide.ValidFrom));
            Assert.That(slidevm.Slides.Count, Is.EqualTo(slide.Slides.Count));


            foreach (var slideShowItem in slide.Slides)
            {
                var slideitemVm = slidevm.Slides.First(c => c.Id == slideShowItem.Id);

                Assert.That(slideitemVm, Is.Not.Null);
                Assert.That(slideitemVm.Name, Is.EqualTo(slideShowItem.Name));
                Assert.That(slideitemVm.Order, Is.EqualTo(slideShowItem.Order));
                Assert.That(slideitemVm.Content, Is.EqualTo(slideShowItem.Content));
            }


            _logger.Information(slidevm.Name);
            _logger.Information(slidevm.Description);
            _logger.Information(slidevm.ValidFrom.ToString());
        }
    }
}
