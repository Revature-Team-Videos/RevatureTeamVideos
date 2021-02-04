using System.Collections.Generic;

namespace VideoShare.Client.Models
{
    public class RoomViewModel
    {
        public List<UserViewModel> Party { get; set; }
        public ChatViewModel Chat { get; set; }
        public UserViewModel Host { get; set; }
        public string VideoUrl { get; set; }
        public string EntityID { get; set; }

        public string GetChannelName()
        {
            return VideoUrl
                .Replace("https://player.twitch.tv/?&channel=", "")
                .Replace("&parent=videos-with-friends.azurewebsites.net", "");
        }

        public override string ToString()
        {
            return $"{Party}, {Chat}, {Host}, {VideoUrl}, {EntityID}";
        }

    }
}