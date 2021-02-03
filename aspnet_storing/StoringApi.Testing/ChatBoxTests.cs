using StoringApi.Service.Models;
using Xunit;

namespace StoringApi.Testing
{
    public class ChatBoxTests
    {
        [Fact]
        private void Test_ChatBoxExists()
        {
            var sut = new ChatBox();
            var actual = sut;

            Assert.IsType<ChatBox>(actual);
            Assert.NotNull(actual);
        }
    }
}