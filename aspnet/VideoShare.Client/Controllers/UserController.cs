using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoShare.Client.Models;

namespace VideoShare.Client.Controllers
{
    [Route("[controller]")]
    [Route("")]
    public class UserController : Controller
    {   
        private string apiUrl = "http://localhost:8080/";
        private HttpClient _http = new HttpClient();
        [HttpGet]
        public IActionResult Home()
        {

            return View("Home");
        }
        [HttpPost("/CreateAccount")]
        public async Task<IActionResult> CreateAccount(string username, string email)
        {
            var response = await _http.GetAsync(apiUrl);
            UserViewModel userview = new UserViewModel();
            userview.Email = email;
            userview.Username = username;
            // create repo method for new user
            _repo.Save();

            return View("logon", userview);
        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string Username)
        {
            UserViewModel userview = new UserViewModel(); 
            var response = await _http.GetAsync(apiUrl + "/users/" + Username);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<UserViewModel> (await response.Content.ReadAsStringAsync());
                userview.Username = content.Username;
                userview.Email = content.Email;
                return View("logonsuccess", userview);
            }
            else return View("logonerror");
        }
        [HttpGet("/NewRoom")]
        public async Task<IActionResult> CreateRoom(string username, string videourl)
        {
            var response = await _http.GetAsync(apiUrl + "/room");
            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<RoomViewModel> (await response.Content.ReadAsStringAsync());
                RoomViewModel roomview = new RoomViewModel();
                roomview.Host = content.GetUser(username);
                return View("VideoSearch");
            }
            else return View("error");
        }
        [HttpPost("/room")]
        public async Task<IActionResult> JoinRoom(string roomid)
        {
            var response = await _http.GetAsync(apiUrl + "/room/" + roomid);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<RoomViewModel> (await response.Content.ReadAsStringAsync());
                RoomViewModel roomview = new RoomViewModel();
                roomview.RoomId = roomid;
                roomview.Party.Add(userview);

                return View("Room");
            }
            else return View("error");
            
        }
        [HttpPost("/room/videos")]
        public async Task<IActionResult> ReturnVideos(string videosearch)
        {
            var response = await _http.GetAsync(apiUrl +"/video" + videosearch);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<VideoViewModel> (await response.Content.ReadAsStringAsync());
                VideoViewModel videoview = new VideoViewModel();
                videoview.Videos = content.Videos;

                return View("SelectVideo", videoview);
            }
            return View("error");            
        }
        [HttpPost("/room/video/{VideoId}")]
        public async Task<IActionResult> SelectVideo(string video)
        {
            var response = await _http.GetAsync(apiUrl +"/video" + video);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<VideoViewModel> (await response.Content.ReadAsStringAsync());
                VideoViewModel videoview = new VideoViewModel();
                videoview.Video = content.Video;

                return View("Video", videoview);
            }
            return View("error"); 
        }
        [HttpGet("/user")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var response = await _http.GetAsync(apiUrl +"/user" + username);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<UserViewModel> (await response.Content.ReadAsStringAsync());
                UserViewModel userview = new UserViewModel();
                userview.Email = content.Email;
                userview.Friends = content.Friends;
                userview.Username = content.Username;
                
                return View("profile", userview);
            }

            return View("error");
        }
    }
}