using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CatalogApi.Models;
using CatalogApi.Models.Automapper;
using CatalogApi.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Serilog;
using Trilly.Tools;

namespace CatalogApi.Tests
{
    [TestFixture]
    public class SqlCallerServiceTests
    {
        private string _connectionString = string.Empty;
        private Mock<IConfigurationService> _configurationService;
        private Serilog.ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _connectionString = StaticSettingsTool.GetTrilloDbAddressForTest();

            _configurationService = new Mock<IConfigurationService>();
            _configurationService.Setup(m => m.GetSqlServerConnectionString()).Returns(_connectionString);

            _logger = new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .CreateLogger();
        }


        [Test]
        [TestCase(1559)]
        [TestCase(1866)]
        public async Task GetSubCategories(int categoryId)
        {
            var service = new CategoryDapperQueryService(_configurationService.Object);

            var subcategories = await service.GetSubCategories(categoryId);

            foreach (var subcategory in subcategories)
            {
                _logger.Information($"{subcategory.Id} : {subcategory.Name} : {subcategory.Slug}");
            }

            Assert.That(subcategories.Count, Is.GreaterThan(0));
        }
    }
}
