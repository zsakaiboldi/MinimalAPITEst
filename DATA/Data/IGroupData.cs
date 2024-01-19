using Modell.Model;

namespace DataAccess.Data
{
    public interface IGroupData
    {
        Task DeleteGroup(int id);
        Task<GroupModel?> GetGroup(int id);
        Task<IEnumerable<GroupModel>> GetGroups();
        Task InsertGroup(GroupModel group);
        Task UpdateGroup(GroupModel group);
    }
}