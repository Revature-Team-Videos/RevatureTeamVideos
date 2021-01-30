using StoringApi.Service.Models;

namespace StoringApi.Service.Interfaces
{
  public interface IUserRepository
  {
    public bool EmailOrUsernameExists(string username, string email);

    public User GetUserByUsername(string username);
    
    public User GetUserByEmail(string email);
  }    
}
