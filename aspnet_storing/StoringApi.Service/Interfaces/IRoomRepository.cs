using System.Collections.Generic;
using StoringApi.Service.Models;

namespace StoringApi.Service.Interfaces
{
  public interface IRoomRepository
  {
    public IEnumerable<User> GetRoomParty(long id);

    public ChatBox GetChat(long id);

    public IEnumerable<Room> GetRoomsByActive(bool active);

    public void AddUserToRoom(long roomid, User user);

    public void RemoveUserFromRoom(long roomid, User user);

    public void CloseRoom(long id);
  }
}
