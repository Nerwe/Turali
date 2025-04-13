using Microsoft.EntityFrameworkCore;
using Turali.Data;
using Turali.Models;

namespace Turali.Repositories
{
    public class ClientRepository(TuraliDBContext context) 
        : RepositoryBase<Client>(context), IClientRepository
    {
        public async Task<Client?> GetClientWithOrdersAsync(int clientId)
        {
            return await _dbSet
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == clientId);
        }
    }
}
