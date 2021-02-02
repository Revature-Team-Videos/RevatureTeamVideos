using StoringApi.Service.Models;
using Xunit;

namespace StoringApi.Testing
{
    public class UserTests
    {
        [Fact]
        private void Test_UserExists()
        {
            var sut = new User();
            var actual = sut;
            Assert.IsType<User>(actual);
            Assert.NotNull(actual);  
        }
        [Fact]
        private void Test_UsernameCreate()
        {
            string name = "testname";
            User test = new User(name);
            
            Assert.Equal(test.Username, name);
        }
    }
}