using Modell.Model;

namespace DATA.Data
{
    public interface IPasswordData
    {
        Task DeletePassword(int id);
        Task<PasswordModel?> GetPassword(int id);
        Task<IEnumerable<PasswordModel>> GetPasswords();
        Task<PasswordModel?> GetUserPassword(int id);
        Task InsertPassword(PasswordModel Password);
        Task UpdatePassword(PasswordModel Password);
    }
}