using Modell.Model;

namespace DataAccess.Data
{
    public interface IStatusData
    {
        Task DeleteStatus(int id);
        Task<StatusModel?> GetStatus(int id);
        Task<IEnumerable<StatusModel>> GetStatuses();
        Task InsertStatus(StatusModel Status);
        Task UpdateStatus(StatusModel Status);
    }
}