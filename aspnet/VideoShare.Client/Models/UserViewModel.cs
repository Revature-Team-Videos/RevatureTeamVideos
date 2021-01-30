using System.Collections.Generic;

namespace VideoShare.Client.Models
{
    public class UserViewModel
    {
        public List<string> VideoSearch { get; set; }
        public string Video { get; set; }
        public VideoViewModel VideoView { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}