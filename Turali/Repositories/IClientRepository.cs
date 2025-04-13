using Turali.Models;

namespace Turali.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetClientWithOrdersAsync(int clientId);
    }
}