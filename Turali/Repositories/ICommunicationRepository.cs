using Turali.Models;

namespace Turali.Repositories
{
    public interface ICommunicationRepository
    {
        Task<IEnumerable<Communication>> GetCommunicationsByClientIdAsync(int clientId);
    }
}