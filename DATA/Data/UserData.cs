using Model;
namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISQLDataAccess _db;

    public UserData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("SELECT * FROM USERS;", new { });

    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "SELECT * FROM USERS WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData("INSERT INTO USERS (USERNAME,NAME,REGISTRATION_DATE,RANK_ID) VALUES(@UserName, @Name, @Registration_Date, @Rank_id);", new { user.UserName,  user.Name, user.Registration_Date,user.Rank_id });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData("UPDATE USERS SET USERNAME = @UserName, NAME = @Name, REGISTRATION_DATE = @Registration_Date, RANK_ID = @Rank_id WHERE ID = @Id;", new { user.UserName, user.Name, user.Registration_Date, user.Rank_id, user.Id });

    public Task DeleteUser(int id) =>
        _db.SaveData("DELETE FROM USERS WHERE ID = @id;", new { Id = id });
    

}
