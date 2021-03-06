using System;
using System.Collections.Generic;
using System.Text.Json;
using keepr_server.Codes;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repoVaultKeep;
    public VaultKeepsService(VaultKeepsRepository repoVaultKeep) { _repoVaultKeep = repoVaultKeep; }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId) { return _repoVaultKeep.GetKeepsByVaultId(vaultId); }
    public VaultKeep Create(VaultKeep newVaultKeep) { return _repoVaultKeep.Create(newVaultKeep); }
    public int Delete(int id, string userId) { PreCheck(id, userId); return _repoVaultKeep.Delete(id); }

    public void PreCheck(int id, string userId)
    {
      VaultKeep vaultKeep = _repoVaultKeep.GetById(id);
      if (vaultKeep == null) { throw new Exception("No vaultKeep by id " + id); }
      if (vaultKeep.CreatorId != userId) { throw new Exception("Not authorized"); }
    }

  }
}