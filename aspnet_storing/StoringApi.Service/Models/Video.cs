using System.Collections.Generic;
using StoringApi.Abstracts;

namespace StoringApi.Service.Models
{
    public class Video : AEntity
    {
        public List<User> Viewers { get; set; }

        public string Username { get; set; }
        
        public Video()
        {
            Viewers = new List<User>();
        }
    }
}
