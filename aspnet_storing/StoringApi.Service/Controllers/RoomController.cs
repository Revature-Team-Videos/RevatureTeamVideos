using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoringApi.Service.Models;
using StoringApi.Service.Repository;

namespace StoringApi.Service.Controllers
{
  [ApiController]
  [Produces("application/json")]
  [Route("[controller]")]
  public class RoomController : ControllerBase
  {
    private UnitOfWork _context;
    
    public RoomController(UnitOfWork context)
    {
      _context = context;
    }

    [Route("/rooms")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var users = _context.GetAll<Room>().ToList();

      return await Task.FromResult(Ok(users));
    }

    [Route("/rooms/{id}")]
    [HttpGet]
    public IActionResult GetRoom(long id)
    {
      var room = _context.Find<Room>(id);

      if(room != null)
      {
        return Ok(room);
      }

      return NotFound(null);
    }

    [Route("/rooms/{id}/chat")]
    [HttpGet]
    public IActionResult GetChat(long id)
    {
      var chat = _context.GetChat(id);

      if(chat != null)
      {
        return Ok(chat);
      }

      return NotFound(null);
    }

    [Route("/rooms/open/")]
    [HttpPost]
    public IActionResult OpenRoom(OpenRoom data)
    {
      var user = _context.GetUserByUsername(data.Username);

      if(user == null)
      {
        return Unauthorized();
      }

      var room = new Room();
      room.Party.Add(user);
      room.Host = room.Party.FirstOrDefault();
      room.VideoUrl = data.VideoUrl;

      _context.Add(room);
      _context.Save();
      return Ok(room);
    }

    [Route("/rooms/open/{username}/{channel}")]
    [HttpPost]
    public IActionResult OpenRoom(string username, string channel)
    {
      var user = _context.GetUserByUsername(username);

      if(user == null)
      {
        return Unauthorized();
      }
      
      var room = new Room();
      room.Party.Add(user);
      room.Host = room.Party.FirstOrDefault();
      room.VideoUrl = $"https://player.twitch.tv/?&channel={channel}&parent=localhost";

      _context.Add(room);
      _context.Save();
      return Ok(room);
    }

    [Route("/rooms/{id}/close")]
    [HttpPost]
    public IActionResult CloseRoom(long id)
    {
      var closed = _context.CloseRoom(id);

      if(closed)
      {
        _context.Save();
        return Ok();
      }

      return NotFound();
    }

    [Route("/rooms/removeuser/{id}/{username}")]
    [HttpPost]
    public IActionResult RemoveUser(long id, string username)
    {
      var user = _context.GetUserByUsername(username);

      var room = _context.Find<Room>(id);

      if(room != null && user != null)
      {
        room.RemoveViewer(user);
        _context.Save();
        return Ok(room);  
      }

      return NotFound();
    }

    [Route("/rooms/adduser/{id}/{username}")]
    [HttpPost]
    public IActionResult AddUser(long id, string username)
    {
      var user = _context.GetUserByUsername(username);

      var room = _context.Find<Room>(id);

      if(room != null && user != null)
      {
        room.AddViewer(user);
        _context.Save();
        return Ok(room);  
      }

      return NotFound();
    }

    [Route("/rooms/list/{active}")]
    [HttpGet]
    public IActionResult GetActiveRooms(bool active)
    {
      var rooms = _context.GetRoomsByActive(active);
      
      return Ok(rooms);
    }

    [Route("/rooms/party/{id}")]
    [HttpGet]
    public IActionResult GetRoomParty(long id)
    {
      var party = _context.GetRoomParty(id);

      if(party != null)
      {
        return Ok(party);
      }

      return NotFound();
    }
  }
}
