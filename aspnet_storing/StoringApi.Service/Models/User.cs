
using System.Collections.Generic;
using StoringApi.Abstracts;

namespace StoringApi.Service.Models
{
    public class User : AEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public User(){}

        public User(string name)
        {
            Username = name;
        }
    }
}
