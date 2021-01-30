using System.Collections.Generic;
using StoringApi.Service.Models;

namespace StoringApi.Service.Interfaces
{
  public interface IMessageRepository
  {
    public List<Message> GetMessagesByUser(User user);

    public List<Message> GetMessagesByUser(User user, int amount);

    public List<Message> GetMessagesByChatID(long id);
  }
}
