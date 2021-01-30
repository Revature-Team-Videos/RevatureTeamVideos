using RepoApi.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
    public class RoomTests
    {
        [Fact]
        private void Test_RoomExists()
        {
            // arrange
            var sut = new RoomModel(); // inference

            // act
            var actual = sut;

            // assert
            Assert.IsType<RoomModel>(actual);
            Assert.NotNull(actual);
        }
    }
}
