using RepoApi.Service.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class ChatBoxControllerTests
    {
        [Fact]
        public class ChatBoxControllerTest
        {
            [Fact]
            public void Test_Constructor()
            {
                var sut = new ChatBoxController();

                Assert.NotNull(sut);
            }
        }
    }
}
