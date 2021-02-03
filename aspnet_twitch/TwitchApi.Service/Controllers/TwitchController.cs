using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TwitchApi.Service.Models;

namespace TwitchApi.Service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TwitchController : ControllerBase
  {
    private HttpClient _http = new HttpClient();

    private readonly IConfiguration _config;

    public TwitchController(IConfiguration configuration)
    {
      _config = configuration;
    }

    [Route("/topstreams")]
    [HttpGet]
    public async Task<IActionResult> GetStreams()
    {
      var response = await _http.PostAsync(GetAuthPath(), null);

      if(!response.IsSuccessStatusCode)
      {
        return Unauthorized();
      }

      var auth_token = await response.Content.ReadAsStringAsync();
      var auth = JsonSerializer.Deserialize<AuthViewModel>(auth_token);

      var secondHttp = new HttpClient();
      secondHttp.DefaultRequestHeaders.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.access_token);
      secondHttp.DefaultRequestHeaders.Add("Client-Id", _config["twitch_client"]);

      var streams_response = await secondHttp.GetAsync("https://api.twitch.tv/helix/streams?first=10");
      
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
      var response = await _http.PostAsync(GetAuthPath(), null);

      if(!response.IsSuccessStatusCode)
      {
        return Unauthorized();
      }

      var auth_token = await response.Content.ReadAsStringAsync();
      var auth = JsonSerializer.Deserialize<AuthViewModel>(auth_token);

      var secondHttp = new HttpClient();
      secondHttp.DefaultRequestHeaders.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.access_token);
      secondHttp.DefaultRequestHeaders.Add("Client-Id", _config["twitch_client"]);

      var videoUrl = "https://api.twitch.tv/helix/videos?user_id=" + id + "?first=1";
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
      var response = await _http.PostAsync(GetAuthPath(), null);

      if(response.IsSuccessStatusCode)
      {
        var text = await response.Content.ReadAsStringAsync();
        return Ok(text);
      }

      return Unauthorized(null);
    }

    private string GetAuthPath()
    {
      var client = _config["twitch_client"];
      var secret = _config["twitch_secret"];
      var oauth_path = $"https://id.twitch.tv/oauth2/token?client_id={client}&client_secret={secret}&grant_type=client_credentials";
      
      return oauth_path;
    }
  }
}
