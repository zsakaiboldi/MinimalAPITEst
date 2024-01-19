using Model;
namespace DataAccess.Data;

public class RankData : IRankData
{

    private readonly ISQLDataAccess _db;

    public RankData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<RankModel>> GetRanks() =>
        _db.LoadData<RankModel, dynamic>("SELECT * FROM RANK;", new { });

    public async Task<RankModel?> GetRank(int id)
    {
        var results = await _db.LoadData<RankModel, dynamic>(
            "SELECT * FROM RANK WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertRank(RankModel Rank) =>
        _db.SaveData("INSERT INTO RANK (RANK) VALUES(@Rank);",
            new { Rank.Rank });

    public Task UpdateRank(RankModel Rank) =>
        _db.SaveData("UPDATE RANK SET RANK = @Rank WHERE ID = @Id",
            new { Rank.Rank, Rank.Id });

    public Task DeleteRank(int id) =>
        _db.SaveData("DELETE FROM RANK WHERE ID = @id;", new { Id = id });
}
