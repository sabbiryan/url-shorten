using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UrlShorten.EntityFrameworkCore;

namespace UrlShorten.UnitTests
{
    public class UrlShortenUnitTestBase
    {
        protected DbContextOptions<AppDbContext> Options;

        public UrlShortenUnitTestBase()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            builder.UseInMemoryDatabase("UrlShortenDb");

            Options = builder.Options;

            //Global Arrange
            ArrangeUrlMaps();
        }


        private void ArrangeUrlMaps()
        {
            using (var context = new AppDbContext(Options))
            {
                var urlMaps = new List<UrlMap>
                {
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 1", RawUrl = "http://test1.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 2", RawUrl = "http://test2.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 3", RawUrl = "http://test3.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 4", RawUrl = "http://test4.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 5", RawUrl = "http://test5.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 6", RawUrl = "http://test6.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 7", RawUrl = "http://test7.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 8", RawUrl = "http://test8.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 9", RawUrl = "http://test9.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 10", RawUrl = "http://test10.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 11", RawUrl = "http://test11.com"},
                    new UrlMap {Id = Guid.NewGuid().ToString(), Title = "Test 12", RawUrl = "http://test12.com"},
                };

                context.AddRange(urlMaps);
                context.SaveChanges();
            }
        }

    }
}
