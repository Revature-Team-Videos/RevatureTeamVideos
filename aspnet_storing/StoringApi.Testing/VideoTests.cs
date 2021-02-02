using StoringApi.Service.Models;
using Xunit;

namespace StoreingApi.Testing
{
    public class VideoTests
    {
        [Fact]
        public void Test_VideoExists()
        {
            var sut = new Video();
            var actual = sut;
            var videos = sut.Viewers;
            Assert.IsType<Video>(actual);
            Assert.NotNull(actual); 
            Assert.NotNull(videos);
        }
    }
}