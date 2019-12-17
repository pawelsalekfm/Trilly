using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PromotorApi.Application.SlideShow.Queries.GetAllSlideShowList;
using PromotorApi.Mappings;
using PromotorApi.Model;
using Serilog;
using Trilly.ViewModels.Promotor;
using Trilly.ViewModels.Responses;

namespace PromotorApi.Tests.Handlers
{
    [TestFixture()]
    public class GetSlideShowListHandlerTests
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
                opts.AddProfile<SlideShowToSlideShowListItemVmMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task GdyMode_All_ZwracaWszystkieEkrany()
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

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });

            _context.SaveChanges();


            Assert.That(_context.SlideShows.Count(), Is.EqualTo(3));

            var handler = new GetSlideShowListHandler(_context, _mapper);

            var command = new GetSlideShowListCommand();
            command.Mode = (int) GetSlideShowListCommandMode.All;

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));
            Assert.That(response.ObjectResponse, Is.TypeOf(typeof(List<SlideShowListItemVm>)));

            var responseList = (List<SlideShowListItemVm>) response.ObjectResponse;

            Assert.That(responseList.Count, Is.EqualTo(_context.SlideShows.Count()));

            foreach (var slideShowListItemVm in responseList)
            {
                var slideShow = _context.SlideShows.FirstOrDefault(c => c.Id == slideShowListItemVm.Id);

                Assert.That(slideShow.Id, Is.EqualTo(slideShowListItemVm.Id));
                Assert.That(slideShow.Name, Is.EqualTo(slideShowListItemVm.Name));
                Assert.That(slideShow.CreationDate, Is.EqualTo(slideShowListItemVm.CreationDate));
                Assert.That(slideShow.LastUpdateDate, Is.EqualTo(slideShowListItemVm.LastUpdateDate));
                Assert.That(slideShow.ValidFrom, Is.EqualTo(slideShowListItemVm.ValidFrom));
                Assert.That(slideShow.ValidTo, Is.EqualTo(slideShowListItemVm.ValidTo));
            }
        }

        [Test]
        public async Task GdyWybranyMode_ZwracaWszystkieEkrany_Z_Wybranym_Mode()
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

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Deleted,
                ValidFrom = DateTime.Now
            });

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Deleted,
                ValidFrom = DateTime.Now
            });

            _context.SaveChanges();


            var handler = new GetSlideShowListHandler(_context, _mapper);

            var command = new GetSlideShowListCommand();
            command.Mode = (int)GetSlideShowListCommandMode.Deleted;

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));
            Assert.That(response.ObjectResponse, Is.TypeOf(typeof(List<SlideShowListItemVm>)));

            var responseList = (List<SlideShowListItemVm>)response.ObjectResponse;

            Assert.That(responseList.Count, Is.EqualTo(_context.SlideShows.Where(c=>c.Status == (int)SlideShowStatusEnum.Deleted).Count()));

            foreach (var slideShowListItemVm in responseList)
            {
                var slideShow = _context.SlideShows.FirstOrDefault(c => c.Id == slideShowListItemVm.Id);

                Assert.That(slideShow.Id, Is.EqualTo(slideShowListItemVm.Id));
                Assert.That(slideShow.Name, Is.EqualTo(slideShowListItemVm.Name));
                Assert.That(slideShow.CreationDate, Is.EqualTo(slideShowListItemVm.CreationDate));
                Assert.That(slideShow.LastUpdateDate, Is.EqualTo(slideShowListItemVm.LastUpdateDate));
                Assert.That(slideShow.ValidFrom, Is.EqualTo(slideShowListItemVm.ValidFrom));
                Assert.That(slideShow.ValidTo, Is.EqualTo(slideShowListItemVm.ValidTo));
            }
        }

        [Test]
        public async Task GdyWybranyMode_I_BrakujeEkranow_Z_Wybranym_Mode_ZwracaPustaListe()
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

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });

            _context.SlideShows.Add(new SlideShow
            {
                CreationDate = DateTime.Now,
                Description = $"Opis {Guid.NewGuid().ToString()}",
                Name = $"Nazwa {Guid.NewGuid().ToString()}",
                Status = (int)SlideShowStatusEnum.Active,
                ValidFrom = DateTime.Now
            });

            _context.SaveChanges();


            var handler = new GetSlideShowListHandler(_context, _mapper);

            var command = new GetSlideShowListCommand();
            command.Mode = (int)GetSlideShowListCommandMode.Deleted;

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.Status, Is.EqualTo(MethodResponseTypeEnum.Ok));
            Assert.That(response.ObjectResponse, Is.TypeOf(typeof(List<SlideShowListItemVm>)));

            var responseList = (List<SlideShowListItemVm>)response.ObjectResponse;

            Assert.That(responseList.Count, Is.EqualTo(_context.SlideShows.Where(c => c.Status == (int)SlideShowStatusEnum.Deleted).Count()));
        }

    }
}
