using System;
using System.Collections.Generic;
using System.Text.Json;
using keepr_server.Codes;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repoVault;
    public VaultsService(VaultsRepository repoVault) { _repoVault = repoVault; }

    public IEnumerable<Vault> GetByProfileId(string profileId) { return _repoVault.GetByProfileId(profileId); }

    public Vault GetById(int id)
    {
      Vault vault = _repoVault.GetById(id);
      if (vault == null) { throw new Exception("No vault with id " + id); }
      return vault;
    }
    public Vault Create(Vault newVault) { return _repoVault.Create(newVault); }
    public Vault Edit(int id, JsonElement edits)
    {
      Vault vault = _repoVault.GetById(id);
      vault.Edit(edits);
      return _repoVault.Edit(vault);
    }
    public int Delete(int id)
    {
      Vault vault = _repoVault.GetById(id);
      if (vault == null) { throw new Exception("No vault with id " + id); }
      return _repoVault.Delete(id);
    }
  }
}