using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoringApi.Abstracts;
using StoringApi.Service.Interfaces;
using StoringApi.Service.Models;

namespace StoringApi.Service.Repository
{
  public class UnitOfWork : IMessageRepository, IRepository, IRoomRepository, IUserRepository
  {
    private Repository _repository;

    private UserRepository _user;

    private MessageRepository _message;

    private RoomRepository _room;

    public UnitOfWork(DbContextOptions<VWFContext> options)
    {
      var context = new VWFContext(options);
      _repository = new Repository(context);
      _user = new UserRepository(context);
      _message = new MessageRepository(context);
      _room = new RoomRepository(context);
    }

    public void Add<T>(T item) where T : AEntity
    {
      _repository.Add<T>(item);
    }

    public void AddUserToRoom(long roomid, User user)
    {
      _room.AddUserToRoom(roomid, user);
    }

    public void CloseRoom(long id)
    {
      _room.CloseRoom(id);
    }

    public bool EmailOrUsernameExists(string username, string email)
    {
      return _user.EmailOrUsernameExists(username, email);
    }

    public T Find<T>(long id) where T : AEntity
    {
      return _repository.Find<T>(id);
    }

    public IEnumerable<T> GetAll<T>() where T : AEntity
    {
      return _repository.GetAll<T>();
    }

    public ChatBox GetChat(long id)
    {
      return _room.GetChat(id);
    }

    public List<Message> GetMessagesByChatID(long id)
    {
      return _message.GetMessagesByChatID(id);
    }

    public List<Message> GetMessagesByUser(User user)
    {
      return _message.GetMessagesByUser(user);
    }

    public List<Message> GetMessagesByUser(User user, int amount)
    {
      return _message.GetMessagesByUser(user, amount);
    }

    public IEnumerable<User> GetRoomParty(long id)
    {
      return _room.GetRoomParty(id);
    }

    public IEnumerable<Room> GetRoomsByActive(bool active)
    {
      return _room.GetRoomsByActive(active);
    }

    public User GetUserByEmail(string email)
    {
      return _user.GetUserByEmail(email);
    }

    public User GetUserByUsername(string username)
    {
      return _user.GetUserByUsername(username);
    }

    public void Remove<T>(T item) where T : AEntity
    {
      _repository.Remove<T>(item);
    }

    public bool RemoveByID<T>(long id) where T : AEntity
    {
      return _repository.RemoveByID<T>(id);
    }

    public void RemoveUserFromRoom(long roomid, User user)
    {
      _room.RemoveUserFromRoom(roomid, user);
    }

    public void Save()
    {
      _repository.Save();
    }
  }
}
