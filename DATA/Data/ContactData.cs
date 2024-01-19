using Modell.Model;
using DataAccess.Data;
using DataAccess;

public class ContactData : IContactData
{
    private readonly ISQLDataAccess _db;

    public ContactData(ISQLDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ContactModel>> GetContacts() =>
        _db.LoadData<ContactModel, dynamic>("SELECT * FROM CONTACT;", new { });

    public async Task<ContactModel?> GetContact(int id)
    {
        var results = await _db.LoadData<ContactModel, dynamic>(
            "SELECT * FROM CONTACT WHERE ID = @id;",
            new { Id = id });
        return results.FirstOrDefault();
    }

    public Task InsertContact(ContactModel contact) =>
        _db.SaveData("INSERT INTO CONTACT (CONTACT_TYPE,USER_ID,CONTACT_VALUE) VALUES(@Contact_type, @User_Id,@Contact_Value);",
            new { contact.Contact_Type, contact.User_Id,contact.Contact_Value });

    public Task UpdateContact(ContactModel contact) =>
        _db.SaveData("UPDATE CONTACT SET CONTACT_TYPE = @Contact_Type, USER_ID = @User_Id, CONTACT_VALUE = @Contact_Value WHERE ID = @Id",
            new { contact.Contact_Type, contact.User_Id, contact.Contact_Value, contact.Id });

    public Task DeleteContact(int id) =>
        _db.SaveData("DELETE FROM CONTACT WHERE ID = @id;", new { Id = id });
}