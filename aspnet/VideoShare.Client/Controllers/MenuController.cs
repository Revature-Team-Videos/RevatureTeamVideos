using System.Collections.Generic;
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
    public async Task<IActionResult> MainMenu(UserViewModel user)
    {
      var response = await _http.GetAsync(storeapiUrl + "/rooms/list/true");
      var mainModel = new MainMenuViewModel();
      mainModel.User.Username = user.Username;

      if(response.IsSuccessStatusCode)
      {
        var json = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<RoomViewModel>>(json);
        mainModel.Rooms = content;
      }

      return View("Room", mainModel);
    }

    [HttpPost("/NewRoom")]
    public async Task<IActionResult> CreateRoom(string channel)
    {
      UserViewModel userview = TempData.Get<UserViewModel>("userview");
      TempData.Keep();

      var response = await _http.PostAsync(storeapiUrl + $"/rooms/open/{userview.Username}/{channel}", null);
      if (response.IsSuccessStatusCode)
      {
        var json = await response.Content.ReadAsStringAsync();

        var room = JsonConvert.DeserializeObject<RoomViewModel>(json);

        return View("ViewingRoom", room);

      }
      else return View("error");
    }

    [HttpGet("/menu/choices")]
    public async Task<IActionResult> MenuChoice(string button)
    {
      if(button == "create")
      {
        var response = await _http.GetAsync(twitchapiUrl + "/topstreams");

        if(!response.IsSuccessStatusCode)
        {
          return View("error");
        }

        var json = await response.Content.ReadAsStringAsync();

        var content = JsonConvert.DeserializeObject<StreamViewModel>(json);

        UserViewModel userview = TempData.Get<UserViewModel>("userview");
        TempData.Keep();

        var streamlistview = new StreamListViewModel() 
        { 
          Username = userview.Username,
          Streams = content
        };
        
        return View("CreateRoom", streamlistview);
      }
      else //Logs out
      {
        return RedirectToAction("Home", "User");
      }
    }

    [HttpPost("/joinroom")]
    public async Task<IActionResult> JoinRoom(string roomid)
    {
        UserViewModel userview = TempData.Get<UserViewModel>("userview");
        TempData.Keep();
        var response = await _http.PostAsync(storeapiUrl + $"/rooms/adduser/{roomid}/{userview.Username}", null);
        if (response.IsSuccessStatusCode)
        {
          var json = await response.Content.ReadAsStringAsync();
          var content = JsonConvert.DeserializeObject<RoomViewModel>(json);

          return View("ViewingRoom", content);
        }
        return View("error");
    }

    [HttpPost("/closeroom")]
    public async Task<IActionResult> CloseRoom(long id)
    {
      UserViewModel userview = TempData.Get<UserViewModel>("userview");
      TempData.Keep();

      var response = await _http.PostAsync(storeapiUrl + $"/rooms/{id}/close", null);
      if(response.IsSuccessStatusCode)
      {
        return View("Room", userview);
      }

      return View();
    }
  }
}
