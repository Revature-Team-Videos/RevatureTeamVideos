using System.Linq;
using StoringApi.Service.Interfaces;
using StoringApi.Service.Models;

namespace StoringApi.Service.Repository
{
  public class UserRepository : IUserRepository
  {
    private VWFContext _context;

    public UserRepository(VWFContext context)
    {
      _context = context;
    }

    public User GetUserByUsername(string username)
    {
      return _context.Set<User>().FirstOrDefault(user => user.Username == username);
    }

    public User GetUserByEmail(string email)
    {
      return _context.Set<User>().FirstOrDefault(user => user.Email == email);
    }

    public bool EmailOrUsernameExists(string username, string email)
    {
      var user = _context.Set<User>().FirstOrDefault(u => u.Username == username 
        || u.Email == email);
      if(user != null)
      {
        return true;
      }
      return false;
    }
  }
}
