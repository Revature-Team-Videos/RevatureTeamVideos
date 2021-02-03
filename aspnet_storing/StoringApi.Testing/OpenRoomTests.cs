using StoringApi.Service.Models;
using Xunit;

namespace StoringApi.Testing
{
    public class OpenRoomTests
    {
        [Fact]
        private void Test_OpenRoomExists()
        {
            var sut = new OpenRoom();
            var actual = sut;

            Assert.IsType<OpenRoom>(actual);
            Assert.NotNull(actual);
        }
    }
}