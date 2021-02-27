using System.ComponentModel.DataAnnotations;

namespace keepr_server.Models
{
  public class Profile
  {
    public Profile()
    {
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }

    public Profile(string email, string name, string picture)
    {
      Email = email;
      Name = name;
      Picture = picture;
    }
  }
}