using Turali.Models;

namespace Turali.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId);
        Task<IEnumerable<Order>> GetOrdersByManagerIdAsync(int managerId);
    }
}