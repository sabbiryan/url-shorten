using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;
using UrlShorten.Web.Controllers;
using Xunit;

namespace UrlShorten.UnitTests.Controllers.TinyUrl
{
    public class TinyUrlControllerTests: UrlShortenUnitTestBase
    {
        readonly TinyUrlController _controller;

        public TinyUrlControllerTests()
        {
            _controller = new TinyUrlController(new TinyUrlService(
                new Repository<UrlMap, string>(new AppDbContext(Options)),
                new ConfigurationRoot(new List<IConfigurationProvider>())));
        }

        #region Test Get All

        [Fact]
        public async Task GetAll_ShouldReturn_10Items()
        {
            //Arrange
            //Use base arrange

            //Act
            var outputs = await _controller.Get(new UrlMapFilterInput());

            //Assert
            Assert.Equal(10, outputs.Items.Count());
        }

        [Fact]
        public async Task GetAll_ShouldReturn2Items()
        {
            //Arrange
            UrlMapPageOutput outputs = await _controller.Get(new UrlMapFilterInput()
            {
                Take = 2
            });

            //Assert
            var result = Assert.IsType<UrlMapPageOutput> (outputs);

            var model = Assert.IsAssignableFrom<UrlMapPageOutput>(result);

            Assert.Equal(2, model.Items.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturn_ListOfUrlMapOutputType()
        {
            var outputs = await _controller.Get(new UrlMapFilterInput());

            var result = Assert.IsType<List<UrlMapOutput>>(outputs);

            Assert.IsAssignableFrom<List<UrlMapOutput>>(result);
        }

        #endregion


        #region Test Get By Id

        [Fact]
        public async Task Get_ShouldReturn_UrlMapOutputType()
        {
            var urlMapOutputs = await _controller.Get(new UrlMapFilterInput());

            var urlMapOutput = await _controller.Get(urlMapOutputs.Items.FirstOrDefault()?.Id);

            var result = Assert.IsType<UrlMapOutput>(urlMapOutput);

            Assert.IsAssignableFrom<UrlMapOutput>(result);
        }

        [Fact]
        public async Task Get_ShouldReturn_UrlMapOutput()
        {
            var urlMapOutputs = await _controller.Get(new UrlMapFilterInput());

            var urlMapOutput = await _controller.Get(urlMapOutputs.Items.FirstOrDefault()?.Id);

            var result = Assert.IsType<UrlMapOutput>(urlMapOutput);

            Assert.NotNull(result);
        }


        [Fact]
        public async Task Get_ShouldReturn_Exception()
        {
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                var task = await _controller.Get("");
            });
        }

        #endregion


        #region Test Post

        [Fact]
        public async Task Post_ShouldReturn_CountWith1More()
        {
            //Arrange
            var urlMapInput = new UrlMapInput()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test",
                Description = "Test",
                RawUrl = "http://test.com"
            };

            var urlMapOutputsBeforeInsert = await _controller.Get(new UrlMapFilterInput()
            {
                Take = Int32.MaxValue
            });

            var urlMapOutput = await _controller.Post(urlMapInput);


            var urlMapOutputsAfterInsert = await _controller.Get(new UrlMapFilterInput()
            {
                Take = Int32.MaxValue
            });


            Assert.Equal(urlMapOutputsBeforeInsert.Items.Count + 1, urlMapOutputsAfterInsert.Items.Count);
        }


        [Fact]
        public async Task Post_ShouldReturn_SameCount()
        {
            //Arrange
            var urlMapInput = new UrlMapInput()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test",
                Description = "Test",
                RawUrl = "http://test1.com"
            };

            var urlMapOutputsBeforeInsert = await _controller.Get(new UrlMapFilterInput()
            {
                Take = Int32.MaxValue
            });


            //Act
            var urlMapOutput = await _controller.Post(urlMapInput);

            var urlMapOutputsAfterInsert = await _controller.Get(new UrlMapFilterInput()
            {
                Take = Int32.MaxValue
            });


            //Assert
            Assert.Equal(urlMapOutputsBeforeInsert.Items.Count, urlMapOutputsAfterInsert.Items.Count);
        }

        #endregion


        #region Test Put


        [Fact]
        public async Task Put_ShouldReturn_SameRawUrl()
        {
            //Arrange
            var urlMapOutputs = await _controller.Get(new UrlMapFilterInput());
            var output = urlMapOutputs.Items.First();


            //Act
            var update = await _controller.Put(output.Id, new UrlMapInput()
            {
                Id = output.Id,
                Title = "Change",
                Description = output.Description,
                RawUrl = output.RawUrl
            });


            //Assert
            Assert.Equal(output.RawUrl, update.RawUrl);
        }

        #endregion



        #region Delet Tests

        [Fact]
        public async Task Delete_ShouldReturn_IsDeletedTrue()
        {
            //Arrange
            var urlMapOutputs = await _controller.Get(new UrlMapFilterInput());
            var output = urlMapOutputs.Items.First();

            //Act
            await _controller.Delete(output.Id);
            var urlMapOutput = await _controller.Get(output.Id);

            //Assert
            Assert.NotEqual(output.IsDeleted, urlMapOutput.IsDeleted);
            Assert.True(urlMapOutput.IsDeleted);
        }


        #endregion
    }
}
