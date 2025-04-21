using Turali.Models;

namespace Turali.Repositories
{
    public interface IManagerRepository : IRepositoryBase<Manager>
    {
        Task<IEnumerable<Manager>> GetActiveManagersAsync();
        Task<double?> GetAverageRatingAsync(int managerId);
    }
}