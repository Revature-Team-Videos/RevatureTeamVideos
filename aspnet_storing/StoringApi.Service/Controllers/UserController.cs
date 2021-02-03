using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StoringApi.Service.Models;
using StoringApi.Service.Repository;

namespace StoringApi.Service.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private UnitOfWork _context;
    
    public UserController(UnitOfWork context)
    {
      _context = context;
    }

    [Route("/users")]
    [HttpGet]
    [EnableCors("AllowAll")]
    public async Task<IActionResult> Get()
    {
      var users = _context.GetAll<User>().ToList();

      return await Task.FromResult(Ok(users));
    }

    [Route("/users/username/{username}")]
    [HttpGet]
    [EnableCors("AllowAll")]
    public IActionResult GetUserName(string username)
    {
      var user = _context.GetUserByUsername(username);

      if(user != null)
      {
        return Ok(user);
      }
      
      return NotFound(null);
    }

    [Route("/users/email/{email}")]
    [HttpGet]
    [EnableCors("AllowAll")]
    public IActionResult GetUserEmail(string email)
    {
      var user = _context.GetUserByEmail(email);

      if(user != null)
      {
        return Ok(user);
      }
      
      return NotFound(null);
    }

    [Route("/users/add")]
    [HttpPost]
    [Authorize]
    [EnableCors("AllowAll")]
    public IActionResult AddUser(User user)
    {
      var emailusernameExists = _context.EmailOrUsernameExists(user.Username, user.Email);

      if(!emailusernameExists)
      {
        User newUser = new User()
        {
          Username = user.Username,
          Email = user.Email
        };
        _context.Add<User>(newUser);
        _context.Save();
        return Ok(newUser);
      }
      
      return Conflict(user);
    }

    [Route("/users/testme")]
    [HttpPost]
    [EnableCors("AllowAll")]
    public IActionResult Test(User user)
    {
      return Ok(user);
    }
  }
}
