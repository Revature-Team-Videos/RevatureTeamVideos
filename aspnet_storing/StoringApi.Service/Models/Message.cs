using System;
using StoringApi.Abstracts;

namespace StoringApi.Service.Models
{
  public class Message : AEntity
  {
    public string Sentence { get; set; }

    public DateTime TimeStamp { get; set; }

    public User User { get; set; }

    public long ChatBoxEntityID { get; set; }

    public Message()
    {
      TimeStamp = DateTime.Now;
    }
  }
}
