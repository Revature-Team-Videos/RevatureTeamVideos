using RepoApi.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace RepoApi.Testing
{
  public class ChatBoxTests
  {
    [Fact]
    private void Test_ChatBoxExists()
    {
      // arrange
      var mes = new ChatBoxModel(); // inference

      // act
      var actual = mes;

      // assert
      Assert.IsType<ChatBoxModel>(actual);
      Assert.NotNull(actual);
    }
  }
}
