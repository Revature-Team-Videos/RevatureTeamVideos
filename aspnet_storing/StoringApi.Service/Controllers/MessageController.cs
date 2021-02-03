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
  public class MessageController : ControllerBase
  {
    private UnitOfWork _context;
    
    public MessageController(UnitOfWork context)
    {
      _context = context;
    }

    [Route("/messages/users")]
    [HttpGet]
    [Authorize]
    [EnableCors("AllowAll")]
    public IActionResult GetMessagesByUser(User user)
    {
      var messages = _context.GetMessagesByUser(user);

      return Ok(messages);
    }

    [Route("/messages/chatid/{id}")]
    [HttpGet]
    [Authorize]
    [EnableCors("AllowAll")]
    public IActionResult GetMessagesByUser(long id)
    {
      var messages = _context.GetMessagesByChatID(id);

      return Ok(messages);
    }
  }
}
