using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VideoShare.Client.Models;

namespace VideoShare.Client.Controllers
{
  [Authorize]
  [Route("[controller]")]
  public class MenuController : Controller
  {
    private readonly IConfiguration _config;
    private HttpClient _http = new HttpClient();

    public MenuController(IConfiguration config)
    {
      _config = config;
    }

    [HttpGet("/MainMenu")]
    public async Task<IActionResult> MainMenu(UserViewModel user)
    {
      var response = await _http.GetAsync(_config["storeAPIURL"] + "/rooms/list/true");
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

      var response = await _http.PostAsync(_config["storeAPIURL"] + $"/rooms/open/{userview.Username}/{channel}", null);
      if (response.IsSuccessStatusCode)
      {
        var json = await response.Content.ReadAsStringAsync();

        var room = JsonConvert.DeserializeObject<RoomViewModel>(json);

        return View("ViewingRoom", new EnterRoomViewModel() {
          Room = room,
          User = userview
        });

      }
      else return View("error");
    }

    [HttpGet("/menu/choices")]
    public async Task<IActionResult> MenuChoice(string button)
    {
      if(button == "create")
      {
        var response = await _http.GetAsync(_config["twitchAPIURL"] + "/topstreams");

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
        var response = await _http.PostAsync(_config["storeAPIURL"] + $"/rooms/adduser/{roomid}/{userview.Username}", null);
        if (response.IsSuccessStatusCode)
        {
          var json = await response.Content.ReadAsStringAsync();
          var room = JsonConvert.DeserializeObject<RoomViewModel>(json);

          return View("ViewingRoom", new EnterRoomViewModel() {
            Room = room,
            User = userview
          });
        }
        return View("error");
    }

    [HttpPost("/closeroom")]
    public async Task<IActionResult> CloseRoom(long id)
    {
      UserViewModel userview = TempData.Get<UserViewModel>("userview");
      TempData.Keep();

      var response = await _http.PostAsync(_config["storeAPIURL"] + $"/rooms/{id}/close", null);
      if(response.IsSuccessStatusCode)
      {
        return RedirectToAction("MainMenu", userview);
      }

      return View();
    }
  }
}
