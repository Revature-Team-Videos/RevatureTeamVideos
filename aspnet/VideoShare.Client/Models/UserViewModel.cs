using System.Collections.Generic;
using VideoShare.Domain.Models;

namespace VideoShare.Client.Models
{
    public class UserViewModel
    {
        public List<Video> VideoSearch { get; set; }
        public string Video { get; set; }
    }
}