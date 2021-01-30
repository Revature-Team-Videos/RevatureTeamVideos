using RepoApi.Service.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class VideoControllerTests
    {
        [Fact]
        public class VideoControllerTest
        {
            [Fact]
            public void Test_Constructor()
            {
                var sut = new VideoController();

                Assert.NotNull(sut);
            }
        }
    }
}