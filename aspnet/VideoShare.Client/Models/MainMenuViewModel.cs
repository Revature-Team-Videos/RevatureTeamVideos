using System.Collections.Generic;

namespace VideoShare.Client.Models
{
  public class MainMenuViewModel
  {
    public UserViewModel User { get; set; }

    public List<RoomViewModel> Rooms { get; set; }

    public MainMenuViewModel()
    {
      User = new UserViewModel();
      Rooms = new List<RoomViewModel>();
    }
  }  
}
