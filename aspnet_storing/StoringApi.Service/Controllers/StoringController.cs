using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace StoringApi.Service
{
  [ApiController]
  [Route("[controller]")]
  public class StoringController : ControllerBase
  {
    private VWFContext _context;
    
    public StoringController(VWFContext context)
    {
      _context = context;
    }

    [Route("/users/")]
    [HttpGet]
    public IActionResult Get()
    {
      var users = _context.Users.ToList();

      return Ok(users);
    }

    [Route("/users/{name}")]
    [HttpGet]
    public IActionResult GetUser(string name)
    {
      var user = _context.Users.FirstOrDefault(user => user.Username == name);

      if(user != null)
      {
        return Ok(user);
      }
      
      return NotFound(null);
    }
  }
}
