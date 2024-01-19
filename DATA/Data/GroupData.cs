using Modell.Model;
namespace DataAccess.Data;

public class GroupData : IGroupData
{
    private readonly ISQLDataAccess _db;

    public GroupData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<GroupModel>> GetGroups() =>
        _db.LoadData<GroupModel, dynamic>("SELECT * FROM GROUPS;", new { });

    public async Task<GroupModel?> GetGroup(int id)
    {
        var results = await _db.LoadData<GroupModel, dynamic>(
            "SELECT * FROM GROUPS WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertGroup(GroupModel group) =>
        _db.SaveData("INSERT INTO GROUPS (GROUP_NAME,DATE,AVALIABLE,MODIFIER_ID) VALUES(@Group_Name, @Date, @Available, @Modifier_Id);",
            new { group.Group_Name, group.Date, group.Available, group.Modifier_Id });

    public Task UpdateGroup(GroupModel group) =>
        _db.SaveData("UPDATE GROUPS SET GROUP_NAME = @Group_Name, DATE = @Date, AVALIABLE = @Available, MODIFIER_ID = @Modifier_Id WHERE ID = @Id",
            new { group.Group_Name, group.Date, group.Available, group.Modifier_Id, group.Id });

    public Task DeleteGroup(int id) =>
        _db.SaveData("DELETE FROM GROUPS WHERE ID = @id;", new { Id = id });


}
