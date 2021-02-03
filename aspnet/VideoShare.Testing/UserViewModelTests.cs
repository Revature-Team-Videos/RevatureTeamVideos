using VideoShare.Client.Models;
using Xunit;

namespace VideoShare.Testing
{
    public class UserViewModelTests
    {
        [Fact]
        private void UserViewModel_Exists()
        {
            var sut = new UserViewModel();
            var actual = sut;
            
            Assert.IsType<UserViewModel>(actual);
        }
    }
}