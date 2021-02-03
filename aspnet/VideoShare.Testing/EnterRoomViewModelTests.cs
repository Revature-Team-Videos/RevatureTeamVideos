using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class EnterRoomViewModelTests
    {
        [Fact]
        private void EnterRoomViewModel_Exists()
        {
            var sut = new EnterRoomViewModel();
            var actual = sut;
            
            Assert.IsType<EnterRoomViewModel>(actual);
        }
    }
}