using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class StreamViewModelTests
    {
        [Fact]
        private void StreamViewModel_Exists()
        {
            var sut = new StreamViewModel();
            var actual = sut;
            
            Assert.IsType<StreamViewModel>(actual);
        }
    }
}