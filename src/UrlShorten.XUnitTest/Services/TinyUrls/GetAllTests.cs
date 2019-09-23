using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls.Dto;
using Xunit;

namespace UrlShorten.XUnitTest.Services.TinyUrls
{
    public class GetAllTests : TinyUrlServiceServiceTestsBase
    {
        private static readonly UrlMap FirstItem = new UrlMap() { Id = Guid.NewGuid().ToString(), Title = "First Item", RawUrl = "htp://test1.com", Identity = 1 };
        private static readonly UrlMap SecondItem = new UrlMap() { Id = Guid.NewGuid().ToString(), Title = "Second Item", RawUrl = "htp://test2.com", Identity = 2 };

        public GetAllTests() : base(new List<UrlMap>() { FirstItem, SecondItem }.AsQueryable())
        {
        }

        [Fact]
        public async Task GetResultModelShouldBeAOfTypeListOfUrlMapOutput()
        {
            var result = await ServiceUnderTest.GetAll(new UrlMapFilterInput());

            var outputs = Assert.IsType<List<UrlMapOutput>>(result);

            Assert.IsAssignableFrom<List<UrlMapOutput>>(outputs);
        }

        [Fact]
        public async Task GetShouldReturnListOfUrlMapOutputs()
        {
            var result = await ServiceUnderTest.GetAll(new UrlMapFilterInput());

            var viewResult = Assert.IsType<List<UrlMapOutput>>(result);

            var model = Assert.IsAssignableFrom<List<UrlMapOutput>>(viewResult);
            Assert.Equal(2, model.Count());
        }
    }
}