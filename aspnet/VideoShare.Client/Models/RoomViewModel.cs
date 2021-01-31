using System.Collections.Generic;

namespace VideoShare.Client.Models
{
    public class RoomViewModel
    {
        public List<UserViewModel> Party { get; set; }
        public ChatViewModel Chat { get; set; }
        public UserViewModel Host { get; set; }
        public VideoViewModel Video { get; set; }
        public string RoomId { get; set; }
    }
}