using System.Collections.Generic;
using StoringApi.Abstracts;

namespace StoringApi.Service.Models
{
    public class ChatBox : AEntity
    {
        public List<Message> Chat { get; set; }

        public long RoomEntityID { get; set; }

        public ChatBox()
        {
            Chat = new List<Message>();
        }
    }
}
