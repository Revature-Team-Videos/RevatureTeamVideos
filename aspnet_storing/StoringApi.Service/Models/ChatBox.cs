using System.Collections.Generic;

namespace VideoShare.Domain.Models
{
    public class ChatBox
    {
        public List<string> Chat { get; set; }
        public ChatBox()
        {
            Chat = new List<string>();
        }
    }
}