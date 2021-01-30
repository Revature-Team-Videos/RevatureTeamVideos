using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using VideoShare.Client.Models;

namespace VideoShare.Client.Controllers
{
    [Route("[controller]")]
    [Route("")]
    public class UserController : Controller
    {   
        private string apiUrl = "http://localhost:8080/ChatBox";
        private HttpClient _http = new HttpClient();
        [HttpGet]
        public IActionResult Home()
        {

            return View("Home");
        }
        [HttpPost("/CreateAccount")]
        public IActionResult CreateAccount(string username, string email)
        {
            UserViewModel userview = new UserViewModel();
            userview.Email = email;
            userview.Username = username;
            User user = new User(userview.Username);
            user.Email = userview.Email;
            // create repo method for new user
            _repo.Update();

            return View("logon", userview);
        }
        [HttpPost("/Login")]
        public IActionResult Login(string Username)
        {
           User user =_repo.GetUser(Username);
            if (user != null)
            {
                UserViewModel userview = new UserViewModel();
                userview.Username = user.Username;
                userview.Email = user.Email;
                return View("logonsuccess", userview);
            }
            else return View("logonerror");
        }
        [HttpGet("/NewRoom")]
        public IActionResult CreateRoom(string username)
        {
          //  User user = _repo.GetUser(username);
            Room room = new Room();
            room.Host = user;
            
            return View("VideoSearch");
        }
        [HttpPost("/Room")]
        public IActionResult JoinRoom(string RoomId, string username)
        {
            User user = _repo.GetUser(username);
            Room room = _repo.GetRoom(RoomId);
            room.Party.Add(user);

            return View("Room");
        }
        [HttpPost("/room/video")]
        public IActionResult ReturnVideos(string videosearch)
        {
            UserViewModel userview = new UserViewModel();
            userview.VideoSearch = _repo.GetVideos(videosearch);

            return View("SelectVideo", userview);
        }
        [HttpPost("/room/video/{VideoId}")]
        public IActionResult SelectVideo(string video)
        {
            UserViewModel userview = new UserViewModel();
            userview.VideoSearch = _repo.GetVideo(video);

            return View("Video", userview);
        }

    }
}