using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideoShare.Client.Models;

namespace VideoShare.Client.Controllers
{
    [Route("[controller]")]
    [Route("")]
    public class UserController : Controller
    {   
        private string storeapiUrl = "http://localhost:7500";
        private string twitchapiUrl = "http://localhost:8000";
        private HttpClient _http = new HttpClient();
        [HttpGet]
        public IActionResult Home()
        {

            return View("Home");
        }
        [HttpPost("/CreateAccount")]
        public async Task<IActionResult> CreateAccount(string username, string email)
        {
            var responseurl = storeapiUrl + "/users/add";
            UserViewModel userview = new UserViewModel();
            userview.Email = email;
            userview.Username = username;
            var model = await Task.Run(() => JsonConvert.SerializeObject(userview));
            var httpcontent = new StringContent(model, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(responseurl, httpcontent);
            if (response.IsSuccessStatusCode)
            {
                return View("logon", userview);
            }
            return View ("error");

        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string Username)
        {
            UserViewModel userview = new UserViewModel(); 
            var response = await _http.GetAsync(storeapiUrl + "/users/username/" + Username);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<UserViewModel> (await response.Content.ReadAsStringAsync());
                userview.Username = content.Username;
                userview.Email = content.Email;
                TempData.Put<UserViewModel>("userview", userview);

                return View("logonsuccess", userview);
            }
            else return View("logonerror");
        }
        [HttpGet("/NewRoom")]
        public async Task<IActionResult> CreateRoom()
        {
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
        [HttpPost("/room")]
        public async Task<IActionResult> JoinRoom(string roomid)
        {
            var response = await _http.GetAsync(storeapiUrl + "/rooms/" + roomid);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<RoomViewModel> (await response.Content.ReadAsStringAsync());
                RoomViewModel roomview = new RoomViewModel();
                roomview.RoomId = roomid;
                UserViewModel userview = TempData.Get<UserViewModel>("userview");
                var model = await Task.Run(() => JsonConvert.SerializeObject(userview));
                var httpcontent = new StringContent(model, Encoding.UTF8, "application/json");
                var response2 = await _http.PostAsync(storeapiUrl + "/rooms/" + roomid + "/" + userview.Username, httpcontent);
                if (response2.IsSuccessStatusCode)
                {
                    return View ("room");
                }
                return View("Error");
            }
            else return View("error");
            
        }
        [HttpPost("/room/videos")]
        public async Task<IActionResult> ReturnVideos(string videosearch)
        {
            var response = await _http.GetAsync(storeapiUrl +"/video" + videosearch);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<VideoViewModel> (await response.Content.ReadAsStringAsync());
                VideoViewModel videoview = new VideoViewModel();
                videoview.Videos = content.Videos;

                return View("SelectVideo", videoview);
            }
            return View("error");            
        }
        [HttpPost("/room/video/{videoid}")]
        public async Task<IActionResult> SelectVideo(string videoid)
        {
            var response = await _http.GetAsync(storeapiUrl +"/twitch/video/" + videoid);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<VideoViewModel> (await response.Content.ReadAsStringAsync());
                VideoViewModel videoview = new VideoViewModel();
                videoview.Video = content.Video;

                return View("Video", videoview);
            }
            return View("error"); 
        }
        [HttpGet("/user")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var response = await _http.GetAsync(storeapiUrl +"/users/username/" + username);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<UserViewModel> (await response.Content.ReadAsStringAsync());
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