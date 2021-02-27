using System;
using System.Collections.Generic;
using System.Text.Json;
using keepr_server.Codes;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repoKeep;

    public KeepsService(KeepsRepository repoKeep) { _repoKeep = repoKeep; }

    public IEnumerable<Keep> GetAll() { return _repoKeep.GetAll(); }

    public IEnumerable<Keep> GetByProfileId(string profileId) { return _repoKeep.GetByProfileId(profileId); }
    public Keep GetById(int id)
    {
      Keep keep = _repoKeep.GetById(id);
      if (keep == null) { throw new Exception("No keep with id " + id); }
      _repoKeep.AddView(id);
      keep.Views++;
      return keep;
    }
    public Keep Create(Keep newKeep) { return _repoKeep.Create(newKeep); }
    public Keep Edit(int id, JsonElement edits, string userId)
    {
      Keep keep = PreCheck(id, userId);
      keep.Edit(edits);
      return _repoKeep.Edit(keep);
    }
    public int Delete(int id, string userId)
    {
      PreCheck(id, userId);
      return _repoKeep.Delete(id);
    }

    public Keep PreCheck(int id, string userId)
    {
      Keep keep = _repoKeep.GetById(id);
      if (keep == null) { throw new Exception("No keep with id " + id); }
      if (keep.CreatorId != userId) { throw new Exception("Not authorized"); }
      return keep;
    }
  }
}