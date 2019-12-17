using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PromotorApi.Application.SlideShow.Queries.GetAllSlideShowList;
using Serilog;
using EnumsNET;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace PromotorApi.Tests.Enums
{
    public enum CatsEnum
    {
        [Description("Kotek Pirat")]
        Pirat = 1,
        [Description("Kotek Kreska")]
        Kreska = 2,
        [Description("Kotek Koksio")]
        Koksio = 3
    }


    [TestFixture()]
    public class EnumsNetTests
    {
        private Serilog.ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = _logger = new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .CreateLogger();
        }

        [Test]
        public void EnumsCats_PobranieAtrybutów()
        {
            var cat = CatsEnum.Koksio;

            string name = cat.GetName();
            _logger.Information($"Name: {name}");
            Assert.That(name, Is.EqualTo("Koksio"));

            var value = cat.GetMember().Value;
            _logger.Information($"Value: {value}");

            var description = cat.GetMember().Attributes.Get<DescriptionAttribute>();
            _logger.Information($"Description: {description.Description}");
            Assert.That(description.Description, Is.EqualTo("Kotek Koksio"));
        }

        [Test]
        public void GetEnumCats_PoNazwie()
        {
            var catName = "Kreska";

            var cat = Enum.Parse<CatsEnum>(catName);

            string name = cat.GetName();
            _logger.Information($"Name: {name}");
            Assert.That(name, Is.EqualTo(catName));

            var value = cat.GetMember().Value;
            _logger.Information($"Value: {value}");

            var description = cat.GetMember().Attributes.Get<DescriptionAttribute>();
            _logger.Information($"Description: {description.Description}");
            Assert.That(description.Description, Is.EqualTo("Kotek Kreska"));
        }

        [Test]
        public void GetEnumCats_PoWartosci()
        {
            var catValue = 1;

            var cat = Enum.Parse<CatsEnum>("1");

            string name = cat.GetName();
            _logger.Information($"Name: {name}");
            Assert.That(name, Is.EqualTo("Pirat"));

            var value = cat.GetMember().Value;
            _logger.Information($"Value: {value}");

            var description = cat.GetMember().Attributes.Get<DescriptionAttribute>();
            _logger.Information($"Description: {description.Description}");
            Assert.That(description.Description, Is.EqualTo("Kotek Pirat"));
        }

        [Test]
        public void GetEnumCats_PoWartosci_Niepoprawny()
        {
            var catValue = 1;

            var cat = Enum.Parse<CatsEnum>("2");

            string name = cat.GetName();
            _logger.Information($"Name: {name}");

            var value = cat.GetMember().Value;
            _logger.Information($"Value: {value}");

            var description = cat.GetMember().Attributes.Get<DescriptionAttribute>();
            _logger.Information($"Description: {description.Description}");


            Assert.That(name, Is.Not.EqualTo("Pirat"));
            Assert.That(description.Description, Is.Not.EqualTo("Kotek Pirat"));
        }


        [Test]
        public void GetEnumCats_PozaZakresem_Niepoprawny()
        {
            var catValue = 5;

            var cat = Enum.Parse<CatsEnum>(Convert.ToString(catValue));

            Assert.That(cat.IsValid(), Is.False);
            Assert.That(cat.IsDefined(), Is.False);
        }
    }
}
