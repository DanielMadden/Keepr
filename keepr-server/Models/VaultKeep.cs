using System.ComponentModel.DataAnnotations;

namespace keepr_server.Models
{
  public class VaultKeep
  {
    public VaultKeep()
    {
    }

    public int Id { get; set; }
    public string CreatorId { get; set; }
    [Required]
    public int VaultId { get; set; }
    [Required]
    public int KeepId { get; set; }

    public VaultKeep(string creatorId, int vaultId, int keepId)
    {
      CreatorId = creatorId;
      VaultId = vaultId;
      KeepId = keepId;
    }
  }
}