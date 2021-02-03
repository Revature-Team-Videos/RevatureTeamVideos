using System.Collections.Generic;
using System.Linq;
using StoringApi.Service.Models;
using Xunit;

namespace StoringApi.Testing
{
    public class RoomTests
    {
        [Fact]
        private void Test_RoomExists()
        {
            var sut = new Room();
            var actual = sut;
            Assert.IsType<Room>(actual);
            Assert.NotNull(actual);
        }
        [Fact]
        private void Test_AddViewer()
        {
            Room test = new Room();
            User user = new User();
            test.AddViewer(user);

            Assert.Equal(user, test.Party.First());
        }
        [Fact]
        private void Test_RemoveViewer()
        {
            Room test = new Room();
            User user = new User();
            test.Party.Add(user);
            test.RemoveViewer(user);
            
            Assert.Empty(test.Party);
        
        }
        [Fact]
        private void Test_CloseRoom()
        {
            Room test = new Room();
            User user = new User();
            test.Party.Add(user);
            test.Host = user;

            test.CloseRoom();

            Assert.Empty(test.Party);
            Assert.False(test.IsActive);
            Assert.Null(test.Host);
        }
        [Fact]
        private void Test_DeleteMessage()
        {
            string name = "dan";
            Room test = new Room();
            User user = new User(name);
            test.Host = user;
            Message message = new Message();
            string chat = "Test message";
            message.Sentence = chat;
            test.RoomChat.Chat.Add(message);
            int messageindex = 0;

            test.DeleteMessage(name, messageindex);

            Assert.Empty(test.RoomChat.Chat);

        }
    }
}