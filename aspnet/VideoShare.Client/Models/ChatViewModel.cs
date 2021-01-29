using System.Collections.Generic;
using VideoShare.Domain.Models;

namespace VideoShare.Client.Models
{
    public class ChatViewModel
    {
        public List<string> Message { get; set; }
        public ChatViewModel ChatView { get; set; }
        public string Username { get; set; }
    }
}