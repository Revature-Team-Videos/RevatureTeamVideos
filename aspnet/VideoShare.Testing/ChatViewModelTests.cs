using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class ChatViewModelTests
    {
        [Fact]
        private void ChatViewModel_Exists()
        {
            var sut = new ChatViewModel();
            var actual = sut;
            
            Assert.IsType<ChatViewModel>(actual);
        }
    }
}