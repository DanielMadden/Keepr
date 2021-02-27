using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace keepr_server.Models
{
  public class Keep
  {
    public Keep()
    {
    }

    public int Id { get; set; }

    public string CreatorId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Img { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Keeps { get; set; }
    public Profile Creator { get; set; }

    public Keep(string creatorId, string name, string description, string img, int views, int shares, Profile creator)
    {
      CreatorId = creatorId;
      Name = name;
      Description = description;
      Img = img;
      Views = views;
      Shares = shares;
      Creator = creator;
    }

    public void Edit(JsonElement edits)
    {
      if (edits.TryGetProperty("name", out JsonElement newName)) { Name = newName.ToString(); }
      if (edits.TryGetProperty("description", out JsonElement newDescription)) { Description = newDescription.ToString(); }
      if (edits.TryGetProperty("img", out JsonElement newImg)) { Img = newImg.ToString(); }
      // if (edits.TryGetProperty("views", out JsonElement newViews)) { Views = newViews.GetInt32(); }
      // if (edits.TryGetProperty("shares", out JsonElement newShares)) { Shares = newShares.GetInt32(); }
    }
  }

  public class VaultKeepViewModel : Keep
  {
    public int VaultKeepId { get; set; }
  }
}