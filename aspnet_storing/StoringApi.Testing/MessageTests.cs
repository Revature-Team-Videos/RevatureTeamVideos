using StoringApi.Service.Models;
using Xunit;

namespace StoringApi.Testing
{
    public class MessageTests
    {
        [Fact]
        private void Test_MessageExists()
        {
            var sut = new Message();
            var actual = sut;

            Assert.IsType<Message>(actual);
            Assert.NotNull(actual);
        }
    }
}