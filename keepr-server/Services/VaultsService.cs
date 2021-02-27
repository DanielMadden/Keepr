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

    public bool CheckPrivate(int id)
    {
      Vault vault = _repoVault.GetById(id);
      if (vault == null) { throw new Exception("No vault by id " + id); }
      return vault.IsPrivate;
    }

    public IEnumerable<Vault> GetByProfileId(string profileId, bool filter = false)
    {
      if (!filter) { return _repoVault.GetByProfileId(profileId); }
      else { return _repoVault.GetByProfileId(profileId, true); }
    }

    public Vault GetById(int id, string userId)
    {
      Vault vault = PreCheck(id, userId, true);
      return vault;
    }
    public Vault GetById(int id)
    {
      return _repoVault.GetById(id);
    }
    public Vault Create(Vault newVault) { return _repoVault.Create(newVault); }
    public Vault Edit(int id, JsonElement edits, string userId)
    {
      Vault vault = PreCheck(id, userId);
      vault.Edit(edits);
      return _repoVault.Edit(vault);
    }
    public int Delete(int id, string userId)
    {
      PreCheck(id, userId);
      return _repoVault.Delete(id);
    }

    public Vault PreCheck(int id, string userId, bool privateCheck = false)
    {
      Vault vault = _repoVault.GetById(id);
      if (vault == null) { throw new Exception("No vault with id " + id); }
      if (privateCheck == true) { if (vault.IsPrivate == true) { if (vault.CreatorId != userId) { throw new Exception("Not authorized"); } } }
      if (privateCheck == false) { if (vault.CreatorId != userId) { throw new Exception("Not authorized"); } }
      return vault;
    }
  }
}