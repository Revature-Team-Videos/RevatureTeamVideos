using System.Collections.Generic;

namespace VideoShare.Domain.Models
{
    public class Video
    {
        public List<User> Viewers { get; set; }
        public string Username { get; set; }
        public Video()
        {
            Viewers = new List<User>();
        }
    }
}