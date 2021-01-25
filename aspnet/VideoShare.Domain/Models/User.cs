
using System.Collections.Generic;

namespace VideoShare.Domain.Models
{
    public class User
    {
        public string Username { get; set; }
        public List<User> Friends { get; set; }
        public string Email { get; set; }
        public User()
        {
            Friends = new List<User>();
        }
        public User(string name)
        {
            Friends = new List<User>();
            Username = name;
        }
        public void AddFriend(string user1, string newfriend)
        {
            // User user = User(user1); 
            // user.Friends.Add(User(newfriend));
        }
    }


}