using System.Collections.Generic;
using VideoShare.Domain.Models;

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