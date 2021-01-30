using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using StoringApi.Abstracts;

namespace StoringApi.Service.Models
{
    public class Room : AEntity
    {
        public List<User> Party { get; set; }

        [NotMapped]
        public User Host { get; set; }

        public ChatBox RoomChat { get; set; }

        public bool IsActive { get; set; }
        
        public Room()
        {
            Party = new List<User>();
            Host = Party.FirstOrDefault(); //Move to user and if user is host also not mapped
            RoomChat = new ChatBox();
        }

        public bool AddViewer(User user)
        {
            bool userExist = false;
            
            foreach(var viewer in Party)
            {
                if(user.Username == viewer.Username)
                {
                    userExist = true;
                }
            }

            if(!userExist && Party.Count <= 5)
            {
                Party.Add(user);
                return true;
            }

            return false;
        }

        public bool RemoveViewer(User user)
        {
            return Party.Remove(user);
        }

        public void CloseRoom()
        {
            Host = null;
            Party = new List<User>();
            IsActive = false;
        }

        public void DeleteMessage(string username, int messageindex)
        {
            if(Host != null && Host.Username == username)
            {
                RoomChat.Chat.RemoveAt(messageindex);;
            }
        }
    }
}
