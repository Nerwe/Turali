using Turali.Models;

namespace Turali.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId);
    }
}