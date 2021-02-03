using StoringApi.Service.Models;
using Xunit;

namespace StoringApi.Testing
{
    public class FriendTests
    {
        [Fact]
        private void Test_FriendExists()
        {
            var sut = new Friend();
            var actual = sut;

            Assert.IsType<Friend>(actual);
            Assert.NotNull(actual);
        }
    }
}