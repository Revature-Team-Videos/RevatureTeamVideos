using Microsoft.AspNetCore.Mvc;

namespace StoringApi.Service.Controllers
{
  public class MessageController : ControllerBase
  {
    private VWFContext _context;
    
    public MessageController(VWFContext context)
    {
      _context = context;
    }
  }
}
