using Modell.Model;

public interface IContactData
{
    Task DeleteContact(int id);
    Task<IEnumerable<ContactModel>> GetContacts();
    Task<ContactModel?> GetContact(int id);
    Task InsertContact(ContactModel contact);
    Task UpdateContact(ContactModel contact);
}