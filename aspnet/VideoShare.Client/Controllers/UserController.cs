using Microsoft.AspNetCore.Mvc;
using VideoShare.Domain.Models;

namespace VideoShare.Client.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {   
        [HttpGet]
        public IActionResult Home()
        {

            return View("Home");
        }
        [HttpGet]
        public IActionResult CreateRoom(string username)
        {
            User user = _repo.GetUser(username);
            Room room = new Room();
            room.Host = user;
            
        }
    }
}