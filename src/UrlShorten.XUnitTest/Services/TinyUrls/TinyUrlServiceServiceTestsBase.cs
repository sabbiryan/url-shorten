using System.Collections.Generic;
using System.Threading.Tasks;
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
                new ConfigurationRoot(new List<IConfigurationProvider>()));
        }

        [Fact]
        public async Task GetAll_ShouldReturn_10Items()
        {
            //Arrange
            

            //Act
            var urlMapFilterInput = new UrlMapFilterInput();
            var urlMapOutputs = await _tinyUrlService.GetAll(urlMapFilterInput);


            //Assert
            Assert.Equal(10, urlMapOutputs.Items.Count);
        }

    }
}
