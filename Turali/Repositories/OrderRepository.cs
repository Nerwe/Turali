using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class OrderRepository(TuraliDBContext context)
        : RepositoryBase<Order>(context), IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _dbSet.Where(o => o.ClientId == clientId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByManagerIdAsync(int managerId)
        {
            return await _dbSet.Where(o => o.ManagerId == managerId).ToListAsync();
        }
    }
}
