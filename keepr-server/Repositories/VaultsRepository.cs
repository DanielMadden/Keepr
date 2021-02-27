using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db) { _db = db; }

    string sqlGet = @"
          SELECT 
              vault.*, 
              profile.*
                  FROM vaults vault
                      JOIN profiles profile
                          ON profile.id = vault.creatorId
    ";

    public IEnumerable<Vault> GetByProfileId(string profileId, bool filter = false)
    {
      string sql = sqlGet + "WHERE vault.creatorId = @profileId";
      if (filter) { sql += " AND vault.isPrivate = 0"; }
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) => { v.Creator = p; return v; }, new { profileId }, splitOn: "id");
    }

    public Vault GetById(int id)
    {
      string sql = sqlGet + "WHERE vault.id = @id";
      Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, p) => { v.Creator = p; return v; }, new { id }, splitOn: "id").FirstOrDefault<Vault>();
      return vault;
    }

    public Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
        (creatorId, name, description, isPrivate)
          VALUES
            (@CreatorId, @Name, @Description, @IsPrivate);

      SELECT LAST_INSERT_ID()
      ";
      Vault returnVault = GetById(_db.ExecuteScalar<int>(sql, newVault));
      return returnVault;
    }

    public Vault Edit(Vault newVault)
    {
      string sql = @"
      UPDATE vaults
        SET
          name = @Name,
          description = @Description,
          isPrivate = @IsPrivate
        WHERE id = @id
      ";
      _db.Execute(sql, newVault);
      return newVault;
    }

    public int Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id";
      return _db.Execute(sql, new { id });
    }
  }
}