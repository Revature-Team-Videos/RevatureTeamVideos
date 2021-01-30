using Microsoft.AspNetCore.Mvc;

namespace StoringApi.Service.Controllers
{
  public class RoomController : ControllerBase
  {
    private VWFContext _context;
    
    public RoomController(VWFContext context)
    {
      _context = context;
    }
  }
}
