using Turali.Models;

namespace Turali.Repositories
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetActiveToursAsync();
    }
}