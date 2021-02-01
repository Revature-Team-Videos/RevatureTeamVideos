using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideoShare.Client.Models;

namespace VideoShare.Client.Controllers
{
  [Route("[controller]")]
  public class MenuController : Controller
  {
    private string storeapiUrl = "http://localhost:7500";
    private string twitchapiUrl = "http://localhost:8000";
    private HttpClient _http = new HttpClient();

    [HttpGet("/MainMenu")]
    public IActionResult MainMenu(UserViewModel user)
    {
      return View("Room", user);
    }

    [HttpGet("/NewRoom")]
    public async Task<IActionResult> CreateRoom()
    {
      //rooms/open/{username}
      UserViewModel userview = TempData.Get<UserViewModel>("userview");
      TempData.Keep();
      var model = await Task.Run(() => JsonConvert.SerializeObject(userview));
      var httpcontent = new StringContent(model, Encoding.UTF8, "application/json");
      var response = await _http.PostAsync(storeapiUrl + "/createroom", httpcontent);
      if (response.IsSuccessStatusCode)
      {
        RoomViewModel roomview = new RoomViewModel();
        roomview.Host = userview;
        return View("VideoSearch");
      }
      else return View("error");
    }
  }
}
