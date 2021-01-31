using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitchApi.Service.Models;

namespace TwitchApi.Service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TwitchController : ControllerBase
  {
    private string oauth_path = "https://id.twitch.tv/oauth2/token?client_id=bqytuqgi8nc9hqoyzxp358hk4oiw8f&client_secret=fp41o28u9xi9uxkgcgx5f5hx5pjgo3&grant_type=client_credentials";

    private string _clientId = "bqytuqgi8nc9hqoyzxp358hk4oiw8f";

    private HttpClient _http = new HttpClient();

    [Route("/topstreams")]
    [HttpGet]
    public async Task<IActionResult> GetStreams()
    {
      var response = await _http.PostAsync(oauth_path, null);

      if(!response.IsSuccessStatusCode)
      {
        return Unauthorized();
      }

      var auth_token = await response.Content.ReadAsStringAsync();
      var auth = JsonSerializer.Deserialize<AuthViewModel>(auth_token);

      var secondHttp = new HttpClient();
      secondHttp.DefaultRequestHeaders.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.access_token);
      secondHttp.DefaultRequestHeaders.Add("Client-Id", _clientId);
      //System.Console.WriteLine(secondHttp.DefaultRequestHeaders.ToString());
      var streams_response = await secondHttp.GetAsync("https://api.twitch.tv/helix/streams?first=5");
      
      if(!streams_response.IsSuccessStatusCode)
      {
        return Unauthorized();
      }

      var streamList = await streams_response.Content.ReadAsStringAsync();

      return Ok(streamList);
    }

    [Route("/twitch/video/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetVideo(string id)
    {
      var response = await _http.PostAsync(oauth_path, null);

      if(!response.IsSuccessStatusCode)
      {
        return Unauthorized();
      }

      var auth_token = await response.Content.ReadAsStringAsync();
      var auth = JsonSerializer.Deserialize<AuthViewModel>(auth_token);

      var secondHttp = new HttpClient();
      secondHttp.DefaultRequestHeaders.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.access_token);
      secondHttp.DefaultRequestHeaders.Add("Client-Id", _clientId);
      //System.Console.WriteLine(secondHttp.DefaultRequestHeaders.ToString());
      //System.Console.WriteLine(id);
      var videoUrl = "https://api.twitch.tv/helix/videos?user_id=" + id + "?first=1";
      //System.Console.WriteLine(videoUrl);
      var video_response = await secondHttp.GetAsync(videoUrl);
      
      if(!video_response.IsSuccessStatusCode)
      {
        return Unauthorized();
      }

      var video = await video_response.Content.ReadAsStringAsync();

      return Ok(video);
    }

    [Route("/testTwitch")]
    [HttpPost]
    public async Task<IActionResult> TestMe()
    {
      var response = await _http.PostAsync(oauth_path, null);

      if(response.IsSuccessStatusCode)
      {
        var text = await response.Content.ReadAsStringAsync();
        return Ok(text);
      }

      return Unauthorized(null);
    }
  }
}
