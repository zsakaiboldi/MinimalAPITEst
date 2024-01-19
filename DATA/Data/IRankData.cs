using Model;

namespace DataAccess.Data
{
    public interface IRankData
    {
        Task DeleteRank(int id);
        Task<RankModel?> GetRank(int id);
        Task<IEnumerable<RankModel>> GetRanks();
        Task InsertRank(RankModel Rank);
        Task UpdateRank(RankModel Rank);
    }
}