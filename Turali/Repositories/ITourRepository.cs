using Turali.Models;

namespace Turali.Repositories
{
    public interface ITourRepository : IRepositoryBase<Tour>
    {
        Task<IEnumerable<Tour>> GetActiveToursAsync();
    }
}