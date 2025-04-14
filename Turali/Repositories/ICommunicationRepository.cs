using Turali.Models;

namespace Turali.Repositories
{
    public interface ICommunicationRepository : IRepositoryBase<Communication>
    {
        Task<IEnumerable<Communication>> GetCommunicationsByClientIdAsync(int clientId);
    }
}