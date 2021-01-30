using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoringApi.Service.Interfaces;
using StoringApi.Service.Models;

namespace StoringApi.Service.Repository
{
  public class MessageRepository : IMessageRepository
  {
    private VWFContext _context;

    public MessageRepository(VWFContext context)
    {
      _context = context;
    }

    public List<Message> GetMessagesByChatID(long id)
    {
      var messages = _context.Set<Message>()
        .Include(m => m.User)
        .Where(m => m.ChatBoxEntityID == id);

      return messages.ToList();
    }

    public List<Message> GetMessagesByUser(User user)
    {
      var messages = _context.Set<Message>()
        .Include(m => m.User)
        .Where(m => m.User.Email == user.Email && m.User.Username == user.Username);

      return messages.ToList();
    }

    public List<Message> GetMessagesByUser(User user, int amount)
    {
      var messages = _context.Set<Message>()
        .Include(m => m.User)
        .Where(m => m.User.Email == user.Email && m.User.Username == user.Username)
        .OrderByDescending(m => m.EntityID)
        .Take(amount);

      return messages.ToList();
    }
  }
}
