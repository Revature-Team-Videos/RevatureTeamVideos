using RepoApi.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class UserTests
    {
        [Fact]
        private void Test_UserExists()
        {
            // arrange
            var sut = new UserModel(); // inference

            // act
            var actual = sut;

            // assert
            Assert.IsType<UserModel>(actual);
            Assert.NotNull(actual);
        }
    }
}
