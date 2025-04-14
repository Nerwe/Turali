using Turali.Models;

namespace Turali.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        Task<Client?> GetClientWithOrdersAsync(int clientId);
    }
}