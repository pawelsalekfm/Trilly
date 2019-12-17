using System;
using AutoMapper;
using EnumsNET;
using NUnit.Framework;
using PromotorApi.Mappings;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Promotor;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace PromotorApi.Tests.Mappings
{
    [TestFixture()]
    public class SlideShowToSlideShowListItemVmMappingProfileTests
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
                opts.AddProfile<SlideShowToSlideShowListItemVmMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Gdy_PoprawneDane_Zwraca_PoprawnyObiekt()
        {
            var slide = new Model.SlideShow();
            slide.Id = 1;
            slide.CreationDate = DateTime.Now;
            slide.Description = $"Opis: {Guid.NewGuid().ToString()}";
            slide.Name = $"Nazwa: {Guid.NewGuid().ToString()}";
            slide.ValidFrom = DateTime.Now;
            slide.ValidTo = DateTime.Now.AddDays(20);

            slide.Status = (int) SlideShowStatusEnum.Deleted;

            var slidevm = _mapper.Map<Model.SlideShow, SlideShowListItemVm>(slide);

            _logger.Information($"{slidevm.Id}");
            _logger.Information($"{slidevm.Name}");
            _logger.Information($"{slidevm.Status}");
            _logger.Information($"{slidevm.StatusString}");
            _logger.Information($"{slidevm.CreationDate.ToString()}");

            if (slidevm.LastUpdateDate.HasValue)
                _logger.Information($"{slidevm.LastUpdateDate.ToString()}");

            _logger.Information($"{slidevm.ValidFrom.ToString()}");

            if (slidevm.ValidTo.HasValue)
                _logger.Information($"{slidevm.ValidTo.Value.ToString()}");

            Assert.That(slidevm, Is.Not.Null);
            Assert.That(slidevm.Id, Is.EqualTo(slide.Id));
            Assert.That(slidevm.Name, Is.EqualTo(slide.Name));
            Assert.That(slidevm.CreationDate, Is.EqualTo(slide.CreationDate));
            Assert.That(slidevm.ValidFrom, Is.EqualTo(slide.ValidFrom));
            Assert.That(slidevm.ValidTo, Is.EqualTo(slide.ValidTo));
            Assert.That(slidevm.Status, Is.EqualTo(slide.Status));

            var statusEnum = Enum.Parse<SlideShowStatusEnum>(Convert.ToString(slidevm.Status));
            Assert.That(statusEnum.IsValid(), Is.True);

            string description = statusEnum.GetMember().Attributes.Get<DescriptionAttribute>().Description;
            Assert.That(slidevm.StatusString, Is.EqualTo(description));
        }
    }
}
