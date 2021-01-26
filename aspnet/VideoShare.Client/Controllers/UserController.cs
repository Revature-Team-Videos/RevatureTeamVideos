using Microsoft.AspNetCore.Mvc;
using VideoShare.Client.Models;
using VideoShare.Domain.Models;

namespace VideoShare.Client.Controllers
{
    [Route("[controller]")]
    [Route("")]
    public class UserController : Controller
    {   
        [HttpGet]
        public IActionResult Home()
        {

            return View("Home");
        }
        [HttpGet("/NewRoom")]
        public IActionResult CreateRoom(string username)
        {
            User user = _repo.GetUser(username);
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

            return View();
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