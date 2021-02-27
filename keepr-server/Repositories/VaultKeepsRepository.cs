using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db) { _db = db; }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
          SELECT 
              keep.*,
              vaultKeep.id as VaultKeepId, 
              profile.*
                  FROM vaultKeeps vaultKeep
                      JOIN keeps keep
                          ON keep.id = vaultKeep.keepId
                      JOIN profiles profile
                          ON profile.id = keep.creatorId
                  WHERE vaultId = @vaultId
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vkvm, p) => { vkvm.Creator = p; return vkvm; }, new { vaultId }, splitOn: "id");
    }

    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
          VALUES
            (@CreatorId, @VaultId, @KeepId);

      SELECT LAST_INSERT_ID()
      ";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    public int Delete(int id)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @id";
      return _db.Execute(sql, new { id });
    }
  }
}