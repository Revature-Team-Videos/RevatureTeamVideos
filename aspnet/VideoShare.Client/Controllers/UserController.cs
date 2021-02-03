using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VideoShare.Client.Models;

namespace VideoShare.Client.Controllers
{
    [Route("[controller]")]
    [Route("")]
    public class UserController : Controller
    {   
        private readonly IConfiguration _config;
        private HttpClient _http = new HttpClient();

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("")]
        public async Task<IActionResult> Home()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                var username = HttpContext.User.FindFirstValue("given_name");
                var email = HttpContext.User.FindFirstValue("preferred_username");
                

                UserViewModel userview = new UserViewModel(); 
                var response = await _http.GetAsync(_config["storeAPIURL"]
                                                        + "/users/username/" + username);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var content = JsonConvert.DeserializeObject<UserViewModel> (json);
                    userview.Username = content.Username;
                    userview.Email = content.Email;
                    TempData.Put<UserViewModel>("userview", userview);
                    return RedirectToAction("MainMenu", "Menu", userview);
                }
                else
                {
                    var responseurl = _config["storeAPIURL"] + "/users/add";
                    userview.Email = email;
                    userview.Username = username;
                    var model = await Task.Run(() => JsonConvert.SerializeObject(userview));
                    var httpcontent = new StringContent(model, Encoding.UTF8, "application/json");
                    await _http.PostAsync(responseurl, httpcontent);
                }

                return RedirectToAction("MainMenu", "Menu", userview);
            }

            return View("Home");
        }

        [HttpPost("/main")]
        public async Task<IActionResult> CreateOrLogin(string username, string email, string button)
        {
            if(button == "login")
            {
                return await Login(username);
            }
            else
            {
                return await CreateAccount(username, email);
            }
        }

        [HttpPost("/CreateAccount")]
        public async Task<IActionResult> CreateAccount(string username, string email)
        {
            var responseurl = _config["storeAPIURL"] + "/users/add";
            UserViewModel userview = new UserViewModel();
            userview.Email = email;
            userview.Username = username;
            var model = await Task.Run(() => JsonConvert.SerializeObject(userview));
            var httpcontent = new StringContent(model, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(responseurl, httpcontent);

            return View("Home", userview);
        }

        [HttpGet("/Login")]
        public async Task<IActionResult> Login(string username)
        {
            UserViewModel userview = new UserViewModel(); 
            var response = await _http.GetAsync(_config["storeAPIURL"] + "/users/username/" + username);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var content = JsonConvert.DeserializeObject<UserViewModel> (json);
                userview.Username = content.Username;
                userview.Email = content.Email;
                TempData.Put<UserViewModel>("userview", userview);

                //return View("Room", userview);
                return RedirectToAction("MainMenu", "Menu", userview);
            }

            //ADD temp data
            return View("Home"); 
        }
        
        [HttpPost("/room/videos")]
        public async Task<IActionResult> ReturnVideos(string videosearch)
        {
            var response = await _http.GetAsync(_config["storeAPIURL"] +"/video" + videosearch);

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
            var response = await _http.GetAsync(_config["storeAPIURL"] +"/twitch/video/" + videoid);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<VideoViewModel> (await response.Content.ReadAsStringAsync());
                VideoViewModel videoview = new VideoViewModel();
                videoview.Video = content.Video;

                return View("Video", videoview);
            }
            return View("error"); 
        }
        
        [Authorize]
        [HttpGet("/profile")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var response = await _http.GetAsync(_config["storeAPIURL"] +"/users/username/" + username);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<UserViewModel> (await response.Content.ReadAsStringAsync());
                UserViewModel userview = new UserViewModel();
                userview.Email = content.Email;
                userview.Username = content.Username;
                
                return View("profile", userview);
            }

            return View("error");
        }
    }
}