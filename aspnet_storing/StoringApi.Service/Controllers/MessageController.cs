using Microsoft.AspNetCore.Mvc;
using StoringApi.Service.Models;
using StoringApi.Service.Repository;

namespace StoringApi.Service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MessageController : ControllerBase
  {
    private UnitOfWork _context;
    
    public MessageController(UnitOfWork context)
    {
      _context = context;
    }

  }
}
