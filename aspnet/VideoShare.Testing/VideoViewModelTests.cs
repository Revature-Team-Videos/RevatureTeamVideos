using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class VideoViewModelTests
    {
        [Fact]
        private void VideoViewModel_Exists()
        {
            var sut = new VideoViewModel();
            var actual = sut;
            
            Assert.IsType<VideoViewModel>(actual);
        }
    }
}