using Modell.Model;
namespace DataAccess.Data;

public class RoleData : IRoleData
{
    private readonly ISQLDataAccess _db;

    public RoleData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<RoleModel>> GetRoles() =>
        _db.LoadData<RoleModel, dynamic>("SELECT * FROM ROLE;", new { });

    public async Task<RoleModel?> GetRole(int id)
    {
        var results = await _db.LoadData<RoleModel, dynamic>(
            "SELECT * FROM ROLE WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertRole(RoleModel Role) =>
        _db.SaveData("INSERT INTO ROLE (ROLE_NAME,DATE,AVALIABLE,MODIFIER_ID) VALUES(@ROLE, @Date, @Available, @Modifier_Id);",
            new { Role.Role, Role.Date, Role.Available, Role.Modifier_Id });

    public Task UpdateRole(RoleModel Role) =>
        _db.SaveData("UPDATE ROLE SET ROLE = @Role, DATE = @Date, AVALIABLE = @Available, MODIFIER_ID = @Modifier_Id WHERE ID = @Id",
            new { Role.Role, Role.Date, Role.Available, Role.Modifier_Id, Role.Id });

    public Task DeleteRole(int id) =>
        _db.SaveData("DELETE FROM ROLE WHERE ID = @id;", new { Id = id });


}
