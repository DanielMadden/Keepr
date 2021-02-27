using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db) { _db = db; }

    private readonly string sqlGet = @"
      SELECT 
          keep.*, 
          profile.*
              FROM keeps keep
                  JOIN profiles profile
                      ON profile.id = keep.creatorId
    ";
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep, Profile, Keep>(sqlGet, (k, p) => { k.Creator = p; return k; }, splitOn: "id");
    }

    public IEnumerable<Keep> GetByProfileId(string profileId)
    {
      string sql = sqlGet + "WHERE keep.creatorId = @profileId";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) => { k.Creator = p; return k; }, new { profileId }, splitOn: "id");
    }

    public Keep GetById(int id)
    {
      string sql = sqlGet + "WHERE keep.id = @id";
      Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, p) => { k.Creator = p; return k; }, new { id }, splitOn: "id").FirstOrDefault<Keep>();
      return keep;
    }

    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
        (creatorId, name, description, img, views, shares)
          VALUES
            (@CreatorId, @Name, @Description, @Img, 0, 0);

      SELECT LAST_INSERT_ID()
      ";
      Keep returnKeep = GetById(_db.ExecuteScalar<int>(sql, newKeep));
      return returnKeep;
    }

    public Keep Edit(Keep newKeep)
    {
      string sql = @"
      UPDATE keeps
        SET
          name = @Name,
          description = @Description,
          img = @Img,
          views = @Views,
          shares = @Shares
        WHERE id = @id
      ";
      _db.Execute(sql, newKeep);
      return newKeep;
    }

    public int Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id";
      return _db.Execute(sql, new { id });
    }
  }
}