using RepoApi.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class VideoTests
    {
        [Fact]
        private void Test_VideoExists()
        {
            // arrange
            var sut = new VideoModel(); // inference

            // act
            var actual = sut;

            // assert
            Assert.IsType<VideoModel>(actual);
            Assert.NotNull(actual);
        }
    }
}
