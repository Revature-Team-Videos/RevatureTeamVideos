namespace TwitchApi.Service.Models
{
  public class AuthViewModel
  {
      public string access_token { get; set; }

      public int expires_in { get; set; }

      public string token_type { get; set; }

      public override string ToString()
      {
        return $"{access_token}, {expires_in}, {token_type}";
      }
  }
}
