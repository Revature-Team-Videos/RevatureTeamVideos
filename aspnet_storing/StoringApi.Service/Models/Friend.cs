using System.ComponentModel.DataAnnotations.Schema;
using StoringApi.Abstracts;

namespace StoringApi.Service.Models
{
  public class Friend : AEntity
  {
    public User User1 { get; set; }

    public long User1EntityID { get; set; }

    public User User2 { get; set; }

    public long User2EntityID { get; set; }

    public string Status { get; set; }
  }
}
