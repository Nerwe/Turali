using Turali.Models;

namespace Turali.Repositories
{
    public interface IManagerRepository
    {
        Task<IEnumerable<Manager>> GetActiveManagersAsync();
    }
}