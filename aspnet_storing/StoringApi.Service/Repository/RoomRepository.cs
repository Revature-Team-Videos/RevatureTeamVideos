using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoringApi.Service.Interfaces;
using StoringApi.Service.Models;

namespace StoringApi.Service.Repository
{
  public class RoomRepository : IRoomRepository
  {
    private VWFContext _context;

    public RoomRepository(VWFContext context)
    {
      _context = context;
    }

    public void AddUserToRoom(long roomid, User user)
    {
      var theUser = _context.Set<User>().Find(user.EntityID);
      var theRoom = GetRoom(roomid);

      if(theUser != null && theRoom != null && theRoom.IsActive)
      {
        bool added = theRoom.AddViewer(user);

        if(added)
        {
          _context.SaveChanges();
        }
      }
    }

    public bool CloseRoom(long id)
    {
      var theRoom = GetRoom(id);
      
      if(theRoom != null && theRoom.IsActive)
      {
        theRoom.CloseRoom();
        _context.SaveChanges();
        return true;
      }

      return false;
    }

    public ChatBox GetChat(long roomid)
    {
      var chatbox = _context.Set<ChatBox>()
        .Include(chat => chat.Chat)
        .FirstOrDefault(chat => chat.EntityID == roomid);

      return chatbox;
    }

    public IEnumerable<User> GetRoomParty(long roomid)
    {
      var users = _context.Set<Room>()
        .Include(room => room.Party)
        .FirstOrDefault(room => room.EntityID == roomid)
        .Party;

      return users;
    }

    public IEnumerable<Room> GetRoomsByActive(bool active)
    {
      var rooms = _context.Set<Room>()
        .Include(room => room.Party)
        .Include(room => room.RoomChat)
        .Where(room => room.IsActive == active);

      return rooms;
    }

    public void RemoveUserFromRoom(long roomid, User user)
    {
      var theRoom = GetRoom(roomid);
      bool removed = theRoom.RemoveViewer(user);
      if(removed)
      {
        _context.SaveChanges();
      }
    }

    private Room GetRoom(long id)
    {
      var theRoom = _context.Set<Room>()
        .Include(room => room.Party)
        .Include(room => room.RoomChat)
        .FirstOrDefault(room => room.EntityID == id);
      
      return theRoom;
    }
  }
}
