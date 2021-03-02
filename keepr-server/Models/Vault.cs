using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace keepr_server.Models
{
  public class Vault
  {
    public Vault()
    {
    }

    public int Id { get; set; }
    public string CreatorId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string Img { get; set; }
    public bool IsPrivate { get; set; } = false;
    public Profile Creator { get; set; }

    public Vault(string creatorId, string name, string description, bool isPrivate, Profile creator)
    {
      CreatorId = creatorId;
      Name = name;
      Description = description;
      IsPrivate = isPrivate;
      Creator = creator;
    }

    public void Edit(JsonElement edits)
    {
      if (edits.TryGetProperty("name", out JsonElement newName)) { Name = newName.ToString(); }
      if (edits.TryGetProperty("description", out JsonElement newDescription)) { Description = newDescription.ToString(); }
      if (edits.TryGetProperty("isPrivate", out JsonElement newPrivacy)) { IsPrivate = newPrivacy.GetBoolean(); }
      if (edits.TryGetProperty("img", out JsonElement newImg)) { Img = newImg.ToString(); }
      if (edits.TryGetProperty("Name", out JsonElement newName2)) { Name = newName2.ToString(); }
      if (edits.TryGetProperty("Description", out JsonElement newDescription2)) { Description = newDescription2.ToString(); }
      if (edits.TryGetProperty("IsPrivate", out JsonElement newPrivacy2)) { IsPrivate = newPrivacy2.GetBoolean(); }
    }
  }
}