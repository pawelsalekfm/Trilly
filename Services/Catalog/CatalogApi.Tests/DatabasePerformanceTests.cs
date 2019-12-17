using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CatalogApi.Models;
using CatalogApi.Models.Automapper;
using CatalogApi.Services;
using NUnit.Framework;
using Serilog;
using Trilly.Tools;

namespace CatalogApi.Tests
{
    [TestFixture()]
    public class DatabasePerformanceTests
    {
        private string _connectionString = string.Empty;
        private CatalogContext _context = null;
        private IMapper _mapper;
        private ProductQueryService _productQueryService;
        private Serilog.ILogger _logger;
        private IProductDapperQueryService _dapperQueryService;


        [SetUp]
        public void SetUp()
        {
            _connectionString = StaticSettingsTool.GetTrilloDbAddressForTest();
            _context = new CatalogContext(_connectionString);

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _productQueryService = new ProductQueryService(_context, _mapper);

            _logger = new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .CreateLogger();

            _dapperQueryService = new ProductDapperQueryService(_mapper);
        }


        [Test]
        [TestCase("IPhone")]
        [TestCase("Etui")]
        [TestCase("Kamera")]
        [TestCase("")]
        public async Task GetProductsByNameQuery(string name)
        {
            using (var benchmark = new BenchmarkLogger("GetProductsByNameQuery - TestWydajnosci", _logger))
            {
                var response = await _productQueryService.GetProductsByName(name);

                Console.WriteLine($"Ilość produktów: {response.Counter}");
                Console.WriteLine($"Minimalna cena: {response.MinPrice}");
                Console.WriteLine($"Maksymalna cena: {response.MaxPrice}");

                Assert.That(response.Counter, Is.GreaterThan(0));
            }
        }

        [Test]
        [TestCase("IPhone")]
        [TestCase("Etui")]
        [TestCase("Kamera")]
        [TestCase("")]
        public async Task GetProductsByNameQueryFromDapper(string name)
        {
            using (var benchmark = new BenchmarkLogger("GetProductsByNameQueryFromDapper - TestWydajnosci", _logger))
            {
                var response = await _dapperQueryService.GetProductsByName(name);

                Console.WriteLine($"Ilość produktów: {response.Counter}");
                Console.WriteLine($"Minimalna cena: {response.MinPrice}");
                Console.WriteLine($"Maksymalna cena: {response.MaxPrice}");

                Assert.That(response.Counter, Is.GreaterThan(0));
            }
        }
    }
}
