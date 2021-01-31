using RepoApi.Service.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class RoomControllerTests
    {
        [Fact]
        public class RoomControllerTest
        {
            [Fact]
            public void Test_Constructor()
            {
                var sut = new RoomController();

                Assert.NotNull(sut);
            }
        }
    }
}