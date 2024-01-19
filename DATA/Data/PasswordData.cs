using DataAccess;
using Microsoft.IdentityModel.Tokens;
using Modell.Model;

namespace DATA.Data;

public class PasswordData : IPasswordData
{
    private readonly ISQLDataAccess _db;

    public PasswordData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<PasswordModel>> GetPasswords() =>
        _db.LoadData<PasswordModel, dynamic>("SELECT * FROM PASSWORD;", new { });

    public async Task<PasswordModel?> GetPassword(int id)
    {
        var results = await _db.LoadData<PasswordModel, dynamic>(
            "SELECT * FROM PASSWORD WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public async Task<PasswordModel?> GetUserPassword(int id)
    {
        var results = await _db.LoadData<PasswordModel, dynamic>(
            "SELECT * FROM PASSWORD WHERE USER_ID = @id;",
            new { User_Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertPassword(PasswordModel Password) =>
        _db.SaveData("INSERT INTO PASSWORD (USER_ID,HASHED_PASSWORD,SALT,CREATED) VALUES(@User_Id,@Hashed_Password,@Salt,@Created);",
            new { Password.User_Id, Password.Hashed_Password, Password.Salt, Password.Created });

    public Task UpdatePassword(PasswordModel Password) =>
        _db.SaveData("UPDATE PASSWORD SET USER_ID = @User_Id, HASHED_PASSWORD = @Hashed_Password, SALT = @Salt, CREATED = @Created WHERE ID = @Id",
            new { Password.User_Id, Password.Hashed_Password, Password.Salt, Password.Created, Password.Id });

    public Task DeletePassword(int id) =>
        _db.SaveData("DELETE FROM PASSWORD WHERE ID = @id;", new { Id = id });
}
