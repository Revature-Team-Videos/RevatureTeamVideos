using RepoApi.Service.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class UserControllerTests
    {
        [Fact]
        public class UserControllerTest
        {
            [Fact]
            public void Test_Constructor()
            {
                var sut = new UserController();

                Assert.NotNull(sut);
            }
        }
    }
}