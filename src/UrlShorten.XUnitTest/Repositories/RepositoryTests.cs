using Xunit;

namespace UrlShorten.UnitTests.Repositories
{

    public class RepositoryTests: UrlShortenUnitTestBase
    {
        [Fact]
        public void Create()
        {
            //// Arrange
            //var testObject = new UrlMap(){Id =  Guid.NewGuid().ToString()};

            //var context = new Mock<AppDbContext>();
            //var dbSetMock = new Mock<DbSet<UrlMap>>();
            //context.Setup(x => x.Set<UrlMap>()).Returns(dbSetMock.Object);
            //dbSetMock.Setup(x => x.Add(It.IsAny<UrlMap>()).Entity).Returns(testObject);

            //// Act
            //var repository = new Repository<UrlMap, string>(context.Object);
            //repository.Create(testObject);

            ////Assert
            //context.Verify(x => x.Set<UrlMap>());
            //dbSetMock.Verify(x => x.Add(It.Is<UrlMap>(y => y == testObject)));

        }

    }
}
