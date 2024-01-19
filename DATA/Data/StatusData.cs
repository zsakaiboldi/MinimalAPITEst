using Modell.Model;
namespace DataAccess.Data;

public class StatusData : IStatusData
{
    private readonly ISQLDataAccess _db;

    public StatusData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<StatusModel>> GetStatuses() =>
        _db.LoadData<StatusModel, dynamic>("SELECT * FROM STATUS;", new { });

    public async Task<StatusModel?> GetStatus(int id)
    {
        var results = await _db.LoadData<StatusModel, dynamic>(
            "SELECT * FROM STATUS WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertStatus(StatusModel Status) =>
        _db.SaveData("INSERT INTO STATUS (DESCRIPTION,CHANGED) VALUES(@Description, @Changed);",
            new { Status.Description, Status.Changed });

    public Task UpdateStatus(StatusModel Status) =>
        _db.SaveData("UPDATE STATUS SET DESCRIPTION = @Description, CHANGED = @Changed WHERE ID = @Id",
            new { Status.Description, Status.Changed, Status.Id });

    public Task DeleteStatus(int id) =>
        _db.SaveData("DELETE FROM STATUS WHERE ID = @id;", new { Id = id });


}
