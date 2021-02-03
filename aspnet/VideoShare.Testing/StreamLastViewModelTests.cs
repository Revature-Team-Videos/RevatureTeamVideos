using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class StreamListViewModelTests
    {
        [Fact]
        private void StreamListViewModel_Exists()
        {
            var sut = new StreamListViewModel();
            var actual = sut;
            
            Assert.IsType<StreamListViewModel>(actual);
        }
    }
}