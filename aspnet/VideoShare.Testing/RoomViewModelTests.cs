using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class RoomViewModelTests
    {
        [Fact]
        private void RoomViewModel_Exists()
        {
            var sut = new RoomViewModel();
            var actual = sut;
            
            Assert.IsType<RoomViewModel>(actual);
        }
        [Fact]
        private void Test_GetChannelName()
        {
            RoomViewModel test = new RoomViewModel();
            test.VideoUrl = "https://player.twitch.tv/?&channel=Jack&parent=videos-with-friends.azurewebsites.net";
            string ChannelName = test.GetChannelName().Trim();

            Assert.Equal("Jack", ChannelName);
        }
    }
}