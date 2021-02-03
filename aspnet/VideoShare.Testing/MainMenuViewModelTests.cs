using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class MainMenuViewModelTests
    {
        [Fact]
        private void MainMenuViewModel_Exists()
        {
            var sut = new MainMenuViewModel();
            var actual = sut;
            
            Assert.IsType<MainMenuViewModel>(actual);
            Assert.NotNull(actual.User);
            Assert.NotNull(actual.Rooms);
        }
    }
}