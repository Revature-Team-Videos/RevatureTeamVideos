using System.Collections.Generic;

namespace RepoApi.Service.Models
{
    public class RoomModel
    {
        public List<UserModel> Party { get; set; }
        public string VideoName { get; set; }
        public string Host { get; set; }
        public ChatBoxModel ChatBox { get; set; }
    }
}