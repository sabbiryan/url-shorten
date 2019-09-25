using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;
using Xunit;

namespace UrlShorten.UnitTests.Services.TinyUrls
{
    public class TinyUrlServiceServiceTestsBase : UrlShortenUnitTestBase
    {
        private readonly ITinyUrlService _tinyUrlService;

        public TinyUrlServiceServiceTestsBase()
        {

            _tinyUrlService = new TinyUrlService(new Repository<UrlMap, string>(new AppDbContext(Options)),
                new ConfigurationRoot(new List<IConfigurationProvider>()), new HttpContextAccessor(),
                new Repository<HitLog, string>(new AppDbContext(Options)));
        }

        [Fact]
        public async Task GetAll_ShouldReturn_PageItems()
        {
            //Arrange
            

            //Act
            var urlMapFilterInput = new UrlMapFilterInput()
            {
                Take = 2,
                Page = 2
            };
            var urlMapOutputs = await _tinyUrlService.GetAll(urlMapFilterInput);


            //Assert
            Assert.Equal(urlMapFilterInput.Take, urlMapOutputs.Items.Count);
            Assert.Equal(2, urlMapFilterInput.Skip);
        }




        [Fact]
        public async Task Get_ShouldReturn_AItem()
        {
            //Arrange
            var urlMapFilterInput = new UrlMapFilterInput()
            {
                Take = 2,
                Page = 2
            };
            var urlMapOutputs = await _tinyUrlService.GetAll(urlMapFilterInput);

            var output = urlMapOutputs.Items.First();


            //Act
            var urlMapOutput = await _tinyUrlService.Get(output.Id);


            //Assert
            Assert.NotNull(urlMapOutput);
            
        }


        [Fact]
        public async Task GetAll_ShouldReturn_CreatedItem()
        {
            //Arrange
            var urlMapInput = new UrlMapInput()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test",
                Description = "Test",
                RawUrl = "http://test.co.com.bd"
            };
            var urlMapPageOutputBefore = await _tinyUrlService.GetAll(new UrlMapFilterInput());
            

            //Act
            var urlMapOutput = await _tinyUrlService.Create(urlMapInput);
            var urlMapPageOutputAfter = await _tinyUrlService.GetAll(new UrlMapFilterInput());

            //Assert
            Assert.NotNull(urlMapOutput);
            Assert.NotNull(urlMapOutput.ShortenUrl);
            Assert.Equal(urlMapPageOutputBefore.TotalCount + 1, urlMapPageOutputAfter.TotalCount);
            Assert.NotEqual(0, urlMapOutput.Identity);
        }

    }
}
