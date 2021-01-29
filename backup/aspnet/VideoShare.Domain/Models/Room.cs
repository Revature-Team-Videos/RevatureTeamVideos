using System.Collections.Generic;
using System.Linq;

namespace VideoShare.Domain.Models
{
    public class Room
    {
        public List<User> Party { get; set; }
        public User Host { get; set; }
        public ChatBox Roomchat { get; set; }
        public Room()
        {
            Party = new List<User>();
            Host = Party.FirstOrDefault(); //Move to user and if user is host also not mapped
            Roomchat = new ChatBox();
        }

        public void DeleteMessage(int messageindex)
        {
            // have to make this host only
            Roomchat.Chat.RemoveAt(messageindex);
        }
    }
}