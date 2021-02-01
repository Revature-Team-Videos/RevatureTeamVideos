using System.Collections.Generic;

namespace VideoShare.Client.Models
{
  public class StreamViewModel
  {
    public IEnumerable<StreamItem> data { get; set; }
  }
  public class StreamItem
  {
    public string id { get; set; }

    public string user_id { get; set; }

    public string user_name { get; set; }

    public string game_id { get; set; }

    public string game_name { get; set; }

    public string type { get; set; }

    public string title { get; set; }

    public long viewer_count { get; set; }

    public string started_at { get; set; }

    public string language { get; set; }

    public string thumbnail_url { get; set; }

    public override string ToString()
    {
      return $"{id}, {user_id}, {user_name}, {game_id}, {title}, {thumbnail_url}";
    }
  }
}
