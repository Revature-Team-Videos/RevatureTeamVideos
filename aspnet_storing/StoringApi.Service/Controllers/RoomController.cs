using Microsoft.AspNetCore.Mvc;
using StoringApi.Service.Repository;

namespace StoringApi.Service.Controllers
{
  public class RoomController : ControllerBase
  {
    private UnitOfWork _context;
    
    public RoomController(UnitOfWork context)
    {
      _context = context;
    }
  }
}
